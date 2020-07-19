using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using request_scheduler.Domain.MauticForms.Dtos;
using request_scheduler.Domain.MauticForms.Interfaces;

namespace request_scheduler.Queues.Consumers
{
    public class SendMauticFormConsumer : ISendMauticFormConsumer
    {
        ConnectionFactory factory { get; set; }
        IConnection connection { get; set; }
        IModel channel { get; set; }

        private IMauticFormService _mauticFormService;

        public SendMauticFormConsumer(IMauticFormService mauticFormService)
        {
            factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            _mauticFormService = mauticFormService;

        }

        public void RegisterToSave()
        {
            channel.QueueDeclare(queue: "mautic-form-to-save", durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var mauticForm = JsonConvert.DeserializeObject<MauticFormRequestDto>(message);
                _mauticFormService.Save(mauticForm);
            };
            channel.BasicConsume(queue: "mautic-form-to-save",
                                 autoAck: true,
                                 consumer: consumer);
        }

    }
}
