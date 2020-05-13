using System.Collections.Generic;
using System.Linq;
using request_scheduler.Domain.MauticForms.Dtos;
using request_scheduler.Domain.MauticForms.Enums;
using request_scheduler.Domain.MauticForms.Interfaces;
using request_scheduler.Domain.MauticForms.Models;
using request_scheduler.Generics.Http;
using request_scheduler.Generics.Http.Enums;

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
            var mauticForm = _mauticFormRepository.GetById(id);

            _mauticFormRepository.Delete(mauticForm);
        }

        public IList<MauticFormDto> Get()
        {
            return _mauticFormRepository.Get().Select(model => new MauticFormDto(model)).ToList();
        }

        public MauticFormDto GetById(long id)
        {
            return new MauticFormDto(_mauticFormRepository.GetById(id));
        }

        public void Save(MauticFormRequestDto dto)
        {
            if (dto.Id == 0)
            {
                var formMautic = new MauticForm(dto.DestinyAddress, dto.HttpMethod, dto.ContentType, dto.Body);
                _mauticFormRepository.Save(formMautic);
            }
            else
            {
                var mauticForm = CreateMauticFormToUpdate(dto);

                _mauticFormRepository.Update(mauticForm);
            }
        }

        private MauticForm CreateMauticFormToUpdate(MauticFormRequestDto dto)
        {
            var mauticForm = _mauticFormRepository.GetById(dto.Id);

            mauticForm.UpdateDestinyAddress(dto.DestinyAddress);
            mauticForm.UpdateHttpMethod(dto.HttpMethod);
            mauticForm.UpdateContentType(dto.ContentType);
            mauticForm.UpdateBody(dto.Body);
            mauticForm.SetUpdatedAt();

            return mauticForm;
        }

        public void Execute()
        {
            var mauticFormPending = _mauticFormRepository.GetAllPending();

            foreach (var mauticForm in mauticFormPending)
            {
                StartProcessingMauticForm(mauticForm);

                Send(mauticForm);
            }
        }

        private MauticForm StartProcessingMauticForm(MauticForm mauticForm)
        {
            mauticForm.UpdateStatus(MauticFormStatus.InProcess);

            _mauticFormRepository.Update(mauticForm);

            return mauticForm;
        }

        private void Send(MauticForm mauticForm)
        {
            var client = new Client(mauticForm.DestinyAddress, mauticForm.ContentType, mauticForm.Body);

            if (mauticForm.HttpMethod == HttpMethod.Post)
            {
                client.Post();
            }
            else
            {
                client.Get();
            }

            mauticForm.UpdateStatus(MauticFormStatus.Sent);
            mauticForm.SetUpdatedAt();

            _mauticFormRepository.Update(mauticForm);
        }
    }
}
