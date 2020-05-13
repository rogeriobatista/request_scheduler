using System.Collections.Generic;
using System.Linq;
using request_scheduler.Data.Context;
using request_scheduler.Domain.MauticForms.Interfaces;
using request_scheduler.Domain.MauticForms.Models;
using request_scheduler.Domain.MauticForms.Enums;

namespace request_scheduler.Data.Repositories
{
    public class MauticFormRepository : IMauticFormRepository
    {
        RequestSchedulerContext _context;
        
        public MauticFormRepository(RequestSchedulerContext context)
        {
            _context = context;
        }

        public void Delete(MauticForm mauticForm)
        {
            _context.MauticFormRequests.Remove(mauticForm);

            _context.SaveChanges();
        }

        public IList<MauticForm> Get()
        {
            return _context.MauticFormRequests.ToList();
        }

        public IList<MauticForm> GetAllPending()
        {
            return _context.MauticFormRequests.Where(x => x.Status == MauticFormStatus.Pending).ToList();
        }

        public MauticForm GetById(long id)
        {
            return _context.MauticFormRequests.FirstOrDefault(x => x.Id == id);
        }

        public void Save(MauticForm mauticForm)
        {
            _context.MauticFormRequests.Add(mauticForm);

            _context.SaveChanges();
        }

        public void Update(MauticForm mauticForm)
        {
            _context.MauticFormRequests.Update(mauticForm);

            _context.SaveChanges();
        }
    }
}
