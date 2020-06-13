using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using request_scheduler.Domain.MauticForms.Dtos;
using request_scheduler.Domain.MauticForms.Enums;
using request_scheduler.Domain.MauticForms.Interfaces;
using request_scheduler.Domain.MauticForms.Models;
using request_scheduler.Generics.Http;
using request_scheduler.Generics.Http.Enums;
using request_scheduler.Queues.Producers;

namespace request_scheduler.Domain.MauticForms.Services
{
    public class MauticFormService : IMauticFormService
    {
        private readonly ISendMauticFormProducer _sendMauticFormProducer;
        private readonly IMauticFormRepository _mauticFormRepository;

        public MauticFormService(ISendMauticFormProducer sendMauticFormProducer, IMauticFormRepository mauticFormRepository)
        {
            _sendMauticFormProducer = sendMauticFormProducer;
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
                string headers = JsonConvert.SerializeObject(dto.Headers);
                var formMautic = new MauticForm(dto.DestinyAddress, dto.HttpMethod, dto.ContentType, headers, dto.Body, dto.SendFrequency);
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
            string headers = JsonConvert.SerializeObject(dto.Headers);
            mauticForm.UpdateHeaders(headers);
            mauticForm.UpdateBody(dto.Body);
            mauticForm.UpdateStatus(dto.Status.Value);
            mauticForm.UpdateSendFrequency(dto.SendFrequency);
            mauticForm.SetUpdatedAt();

            return mauticForm;
        }

        public void Send(MauticForm mauticForm)
        {
            var client = new Client(mauticForm.DestinyAddress, mauticForm.ContentType, mauticForm.Headers, mauticForm.Body);
            try
            {
                if (mauticForm.HttpMethod == HttpMethod.Post)
                {
                    client.Post();
                }
                else
                {
                    client.Get();
                }
            }
            catch (Exception ex)
            {
                mauticForm.UpdateStatus(MauticFormStatus.Failed);
                mauticForm.SetUpdatedAt();

                _mauticFormRepository.Update(mauticForm);
            }
        }

        public void Enqueue(MauticFormSendFrequency sendFrequency, int packageSize)
        {
            var mauticFormPending = _mauticFormRepository.GetAllPending(sendFrequency, packageSize);

            foreach (var mauticForm in mauticFormPending)
            {
                _sendMauticFormProducer.BasicPublic(mauticForm);
                StartProcessingMauticForm(mauticForm);
            }
        }

        private MauticForm StartProcessingMauticForm(MauticForm mauticForm)
        {
            mauticForm.UpdateStatus(MauticFormStatus.Sent);
            mauticForm.SetUpdatedAt();

            _mauticFormRepository.Update(mauticForm);

            return mauticForm;
        }
    }
}
