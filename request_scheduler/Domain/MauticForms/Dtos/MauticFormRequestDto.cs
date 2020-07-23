using System.Collections.Generic;
using request_scheduler.Domain.MauticForms.Enums;
using request_scheduler.Generics.Http.Enums;

namespace request_scheduler.Domain.MauticForms.Dtos
{
    public class MauticFormRequestDto
    {
        public long Id { get; set; }

        public string DestinyAddress { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public string ContentType { get; set; }

        public List<MauticFormHeaderDto> Headers { get; set; }

        public string Body { get; set; }

        public MauticFormStatus? Status { get; set; }

        public string CronId { get; set; }

        public int SendFrequency { get; set; }
    }
}
