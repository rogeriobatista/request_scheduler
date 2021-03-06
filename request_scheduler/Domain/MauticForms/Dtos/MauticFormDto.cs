﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using request_scheduler.Domain.MauticForms.Enums;
using request_scheduler.Domain.MauticForms.Models;
using request_scheduler.Generics.Http.Enums;

namespace request_scheduler.Domain.MauticForms.Dtos
{
    public class MauticFormDto
    {
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string DestinyAddress { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public string ContentType { get; set; }

        public List<MauticFormHeaderDto> Headers { get; set; }

        public string Body { get; set; }

        public MauticFormStatus Status { get; set; }

        public string CronId { get; set; }

        public MauticFormDto(MauticForm model)
        {
            Id = model.Id;
            CreatedAt = model.CreatedAt;
            UpdatedAt = model.UpdatedAt;
            DestinyAddress = model.DestinyAddress;
            HttpMethod = model.HttpMethod;
            ContentType = model.ContentType;
            Headers = JsonConvert.DeserializeObject<List<MauticFormHeaderDto>>(model.Headers);
            Body = model.Body;
            Status = model.Status;
            CronId = CronId;
        }
    }
}
