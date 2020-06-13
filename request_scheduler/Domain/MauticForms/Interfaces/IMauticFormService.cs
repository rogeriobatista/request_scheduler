using System;
using System.Collections.Generic;
using request_scheduler.Domain.MauticForms.Dtos;
using request_scheduler.Domain.MauticForms.Enums;
using request_scheduler.Domain.MauticForms.Models;

namespace request_scheduler.Domain.MauticForms.Interfaces
{
    public interface IMauticFormService
    {
        IList<MauticFormDto> Get();

        MauticFormDto GetById(long id);

        void Save(MauticFormRequestDto dto);

        void Delete(long id);

        void Enqueue(MauticFormSendFrequency sendFrequency, int packageSize);

        void Send(MauticForm mauticForm);
    }
}
