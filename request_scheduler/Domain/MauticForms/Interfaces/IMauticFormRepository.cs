using System;
using System.Collections.Generic;
using request_scheduler.Domain.MauticForms.Dtos;

namespace request_scheduler.Domain.MauticForms.Interfaces
{
    public interface IMauticFormRepository
    {
        IList<MauticFormDto> GetAllPending();

        IList<MauticFormDto> Get();

        MauticFormDto GetById(long id);

        void Save(MauticFormRequestDto dto);

        void Update(MauticFormRequestDto dto);

        void Delete(long id);
    }
}
