using request_scheduler.Domain.MauticForms.Models;

namespace request_scheduler.Queues.Producers
{
    public interface ISendMauticFormProducer
    {
        void BasicPublic(MauticForm mauticForm);
    }
}
