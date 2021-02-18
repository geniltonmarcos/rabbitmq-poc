using RabbitMQ.Client;
using System;
using System.Text;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testando o envio de mensagens para uma Fila do RabbitMQ");

            string connectionString = "amqp://guest:guest@localhost:5672/";
            string queueName = "Fila_01";

            try
            {
                var factory = new ConnectionFactory()
                {
                    Uri = new Uri(connectionString)
                };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: queueName,
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                for (int i = 0; i < 100000; i++)
                {
                    channel.BasicPublish(exchange: "",
                                         routingKey: queueName,
                                         basicProperties: null,
                                         body: Encoding.UTF8.GetBytes($"Mensagem {i}"));
                    Console.WriteLine($"[Mensagem enviada] {i}");
                }
                Console.WriteLine("Concluido o envio de mensagens");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exceção: {ex.GetType().FullName} | " + $"Mensagem: {ex.Message}");
            }
        }
    }
}
