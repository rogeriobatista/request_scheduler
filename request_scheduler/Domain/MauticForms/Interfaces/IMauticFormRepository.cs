using System.Collections.Generic;
using request_scheduler.Domain.MauticForms.Models;

namespace request_scheduler.Domain.MauticForms.Interfaces
{
    public interface IMauticFormRepository
    {
        IList<MauticForm> GetAllPending();

        IList<MauticForm> Get();

        MauticForm GetById(long id);

        void Save(MauticForm model);

        void Update(MauticForm model);

        void Delete(MauticForm mauticForm);
    }
}
