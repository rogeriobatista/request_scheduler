using System;
using System.Collections.Generic;
using System.Linq;
using request_scheduler.Data.Context;
using request_scheduler.Domain.MauticForms.Dtos;
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

        public void Delete(long id)
        {
            var mauticForm = _context.MauticFormRequests.Find(id);
            _context.MauticFormRequests.Remove(mauticForm);
            _context.SaveChanges();
        }

        public IList<MauticFormDto> Get()
        {
            return _context.MauticFormRequests.Select(model => new MauticFormDto(model)).ToList();
        }

        public IList<MauticFormDto> GetAllPending()
        {
            return _context.MauticFormRequests.Where(x => x.Status == MauticFormStatus.Pending).Select(model => new MauticFormDto(model)).ToList();
        }

        public MauticFormDto GetById(long id)
        {
            var mauticForm = _context.MauticFormRequests.FirstOrDefault(x => x.Id == id);

            if (mauticForm == null) return null;

            return new MauticFormDto(mauticForm);
        }

        public void Save(MauticFormRequestDto dto)
        {
            var mauticForm = new MauticForm(dto.DestinyAddress, dto.Body);

            _context.MauticFormRequests.Add(mauticForm);

            _context.SaveChanges();
        }

        public void Update(MauticFormRequestDto dto)
        {
            var mauticForm = _context.MauticFormRequests.Find(dto.Id);

            mauticForm.SetUpdatedAt();
            mauticForm.UpdateDestinyAddress(dto.DestinyAddress);
            mauticForm.UpdateBody(dto.Body);
            mauticForm.UpdateStatus(dto.Status.Value);

            _context.MauticFormRequests.Update(mauticForm);

            _context.SaveChanges();
        }
    }
}
