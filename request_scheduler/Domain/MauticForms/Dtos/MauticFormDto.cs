using System;
using request_scheduler.Domain.MauticForms.Enums;
using request_scheduler.Domain.MauticForms.Models;

namespace request_scheduler.Domain.MauticForms.Dtos
{
    public class MauticFormDto
    {
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string DestinyAddress { get; set; }

        public string Body { get; set; }

        public MauticFormStatus Status { get; set; }

        public MauticFormDto(MauticForm model)
        {
            Id = model.Id;
            CreatedAt = model.CreatedAt;
            UpdatedAt = model.UpdatedAt;
            DestinyAddress = model.DestinyAddress;
            Body = model.Body;
            Status = model.Status;
        }
    }
}
