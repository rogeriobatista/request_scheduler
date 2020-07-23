using System.Collections.Generic;
using System.Threading.Tasks;
using request_scheduler.Domain.MauticForms.Models;

namespace request_scheduler.Domain.MauticForms.Interfaces
{
    public interface IMauticFormRepository
    {
        IList<MauticForm> GetAllPending(string cronId, int packageSize);

        IList<MauticForm> Get();

        MauticForm GetById(long id);

        void Save(MauticForm model);

        Task Update(MauticForm model);

        void Delete(MauticForm mauticForm);
    }
}
