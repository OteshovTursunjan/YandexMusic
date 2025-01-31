using Dapper;
using Npgsql;
using Quartz;
using YandexMusic.Application.Quartz;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic
{
    public class DailyLogging(IConfiguration configuration, RabbitConsumer consumer, ILogger<DailyLogging> logger) : IJob
    {

        private readonly RabbitConsumer _consumer = consumer;
        private readonly string _connectionString = "Server=localhost;Port=5432;Database=YandexF;User Id=postgres;Password=123";


        private readonly ILogger<DailyLogging> _logger = logger;

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Starting RabbitMQ message processing...");

            try
            {
                var messages = _consumer.ReadLogsInRabbit();

                if (messages.Count == 0)
                {
                    _logger.LogInformation("No messages to process.");
                    return;
                }

                _logger.LogInformation($"Processing {messages.Count} messages...");

                foreach (var message in messages)
                {
                    await SaveToDatabase(message);
                }

                _logger.LogInformation("All messages processed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing RabbitMQ messages.");
            }
        }
        private async Task SaveToDatabase(Logging message)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            var query = @"
        INSERT INTO Logging (Level, Message, Exception, Timestamp)
        VALUES (@Level, @Message, @Exception, @Timestamp)";

            var parameters = new Logging
            {
                Level = message.Level,
                Message = message.Message,
                Exception = message.Exception,
                Timestamp = message.Timestamp,
                
            };
            
            int res = await connection.ExecuteAsync(query, parameters);

            _logger.LogInformation($"Saved to database: {message.Message}");
        }
    }
}
