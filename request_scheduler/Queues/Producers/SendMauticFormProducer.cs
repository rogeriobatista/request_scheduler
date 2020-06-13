using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using request_scheduler.Domain.MauticForms.Models;

namespace request_scheduler.Queues.Producers
{
    public class SendMauticFormProducer : ISendMauticFormProducer
    {
        public void BasicPublic(MauticForm mauticForm)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "mautic-form",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var mauticFormToJson = JsonConvert.SerializeObject(mauticForm);
            var body = Encoding.UTF8.GetBytes(mauticFormToJson);

            channel.BasicPublish(exchange: "",
                                 routingKey: "mautic-form",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
