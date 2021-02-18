using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Aguardando mensagens...");

            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672/")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "Fila_01",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (object sender, BasicDeliverEventArgs e) => {

                int timeSleep = new Random().Next(10, 1500);
                Console.WriteLine(
                    $"[Nova mensagem | {timeSleep} | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                    Encoding.UTF8.GetString(e.Body.ToArray()));

                Thread.Sleep(timeSleep);
                channel.BasicAck(e.DeliveryTag, false);
            };

            channel.BasicConsume(
                queue: "Fila_01",
                autoAck: false,
                consumer: consumer);

            Console.ReadLine();
        }
    }
}
