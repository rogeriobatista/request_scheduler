using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using request_scheduler.Domain.MauticForms.Dtos;
using request_scheduler.Domain.MauticForms.Models;

namespace request_scheduler.Queues.Producers
{
    public class SendMauticFormProducer : ISendMauticFormProducer
    {
        public void BasicPublish(MauticForm mauticForm)
        {
            var message = JsonConvert.SerializeObject(mauticForm);

            BasicPublish(message, "mautic-form", "mautic-form");
        }

        public void PublishToSave(MauticFormRequestDto dto)
        {
            var message = JsonConvert.SerializeObject(dto);

            BasicPublish(message, "mautic-form-to-save", "mautic-form-to-save");
        }

        private void BasicPublish(string message, string queue, string routingKey)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
