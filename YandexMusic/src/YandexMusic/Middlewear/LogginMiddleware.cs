using Microsoft.AspNetCore.Connections;
using Minio.DataModel.Tags;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Middlewear
{
    public class LogginMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogginMiddleware> _logger;
        private readonly ConnectionFactory _factory;

        public LogginMiddleware(RequestDelegate next, ILogger<LogginMiddleware> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _factory = new ConnectionFactory
            {
                Uri = new Uri(configuration.GetValue<string>("RabbitMQ:ConnectionString") ?? "amqp://guest:guest@localhost:5672")
            };
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var startTime = DateTime.UtcNow;
            var requestBody = string.Empty;

            context.Request.EnableBuffering();

            using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
            {
                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;  // Reset the position
            }

            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                await SendLogToRabbitMQ(new Logging
                {
                    Level = "Information",
                    Message = $"Request {context.Request.Method} {context.Request.Path}",
                    Timestamp = startTime
                });

                await _next(context);

                responseBody.Position = 0;
                var responseContent = await new StreamReader(responseBody).ReadToEndAsync();
                responseBody.Position = 0;

                await SendLogToRabbitMQ(new Logging
                {
                    Level = "Information",
                    Message = $"Response {context.Response.StatusCode} - Processing Time: {(DateTime.UtcNow - startTime).TotalMilliseconds}ms",
                    Timestamp = DateTime.UtcNow
                });

                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");

                await SendLogToRabbitMQ(new Logging
                {
                    Level = "Error",
                    Message = $"Error processing {context.Request.Method} {context.Request.Path}",
                    Exception = ex.ToString(),
                    Timestamp = DateTime.UtcNow
                });

                context.Response.Body = originalBodyStream;
                throw;
            }
        }

        private async Task SendLogToRabbitMQ(Logging logEntry)
        {
            try
            {
                using var connection = _factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.ExchangeDeclare("logs_exchange", ExchangeType.Direct, durable: true);

                channel.QueueDeclare(
                    queue: "log_queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                channel.QueueBind("log_queue", "logs_exchange", logEntry.Level.ToLower());

                var message = JsonSerializer.Serialize(logEntry);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;  // Make message persistent

                channel.BasicPublish(
                    exchange: "logs_exchange",
                    routingKey: logEntry.Level.ToLower(),
                    basicProperties: properties,
                    body: body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send log to RabbitMQ");
            }
        }

    }
}
