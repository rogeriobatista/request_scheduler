using System;
using System.Collections.Generic;
using request_scheduler.Domain.MauticForms.Dtos;
using request_scheduler.Domain.MauticForms.Interfaces;

namespace request_scheduler.Domain.MauticForms.Services
{
    public class MauticFormService : IMauticFormService
    {
        private readonly IMauticFormRepository _mauticFormRepository;

        public MauticFormService(IMauticFormRepository mauticFormRepository)
        {
            _mauticFormRepository = mauticFormRepository;
        }

        public void Delete(long id)
        {
            _mauticFormRepository.Delete(id);
        }

        public IList<MauticFormDto> Get()
        {
            return _mauticFormRepository.Get();
        }

        public MauticFormDto GetById(long id)
        {
            return _mauticFormRepository.GetById(id);
        }

        public void Save(MauticFormRequestDto dto)
        {
            _mauticFormRepository.Save(dto);
        }

        public void Update(MauticFormRequestDto dto)
        {
            _mauticFormRepository.Update(dto);
        }

        public void Execute()
        {
            var mauticFormPending = _mauticFormRepository.GetAllPending();

            foreach (var mauticForm in mauticFormPending)
            {
                Send(mauticForm);
            }
        }

        private void Send(MauticFormDto mauticForm)
        {
            //TODO
        }
    }
}
