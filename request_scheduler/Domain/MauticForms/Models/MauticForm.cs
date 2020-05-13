using System;
using request_scheduler.Domain.MauticForms.Enums;
using request_scheduler.Generics.Http.Enums;

namespace request_scheduler.Domain.MauticForms.Models
{
    public class MauticForm
    {
        public long Id { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        public string DestinyAddress { get; private set; }

        public HttpMethod HttpMethod { get; private set; }

        public string ContentType { get; private set; }

        public string Body { get; private set; }

        public MauticFormStatus Status { get; private set; }

        protected MauticForm() { }

        public MauticForm(string destinyAddress, HttpMethod httpMethod, string contentType, string body)
        {
            CreatedAt = DateTime.Now;
            Status = MauticFormStatus.Pending;
            DestinyAddress = destinyAddress;
            HttpMethod = httpMethod;
            ContentType = contentType;
            Body = body;
        }

        public void SetUpdatedAt()
        {
            UpdatedAt = DateTime.Now;
        }

        public void UpdateDestinyAddress(string destinyAddress)
        {
            DestinyAddress = destinyAddress;
        }

        public void UpdateHttpMethod(HttpMethod httpMethod)
        {
            HttpMethod = httpMethod;
        }

        public void UpdateContentType(string contentType)
        {
            ContentType = contentType;
        }

        public void UpdateBody(string body)
        {
            Body = body;
        }

        public void UpdateStatus(MauticFormStatus status)
        {
            Status = status;
        }
    }
}
