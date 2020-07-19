using request_scheduler.Domain.MauticForms.Dtos;
using request_scheduler.Domain.MauticForms.Models;

namespace request_scheduler.Queues.Producers
{
    public interface ISendMauticFormProducer
    {
        void BasicPublish(MauticForm mauticForm);
        void PublishToSave(MauticFormRequestDto dto);
    }
}
