using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace rabbitMQ_receiver_app.Services
{
    public static class MessageService
    {
        public static async Task<string> ReceiveMessageAsync()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "test", durable: false, exclusive: false,
                    autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                string message = null;

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(body.Length);
                    Console.WriteLine(message);
                };

                channel.BasicConsume(queue: "test",
                    autoAck: true, consumer: consumer);

                return message;
            }
        }
    }
}
