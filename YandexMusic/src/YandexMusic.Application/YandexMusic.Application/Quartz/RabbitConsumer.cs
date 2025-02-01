using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexMusics.Core.Entities.Music;

namespace YandexMusic.Application.Quartz
{
  public  class RabbitConsumer
    {
        public List<Logging> ReadLogsInRabbit()
        {
            var logs = new List<Logging>();

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "log_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    var log = JsonConvert.DeserializeObject<Logging>(message);
                    if (log != null)
                    {
                        logs.Add(log);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка десериализации: {ex.Message}");
                }
            };

            channel.BasicConsume(queue: "log_queue", autoAck: true, consumer: consumer);

            
            System.Threading.Thread.Sleep(2000);

            return logs;
        }
    }
}
