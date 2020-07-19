using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using request_scheduler.Domain.MauticForms.Dtos;
using request_scheduler.Domain.MauticForms.Interfaces;

namespace request_scheduler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MauticFormController : ControllerBase
    {
        private readonly IMauticFormService _mauticFormService;

        public MauticFormController(IMauticFormService mauticFormService)
        {
            _mauticFormService = mauticFormService;
        }

        [HttpGet]
        public IList<MauticFormDto> Get()
        {
            return _mauticFormService.Get();
        }

        [HttpGet("{id}")]
        public MauticFormDto Get(long id)
        {
            return _mauticFormService.GetById(id);
        }

        [HttpPost]
        public void Save([FromBody] MauticFormRequestDto dto)
        {
            _mauticFormService.Enqueue(dto);
        }

        [HttpDelete]
        public void Delete(long id)
        {
            _mauticFormService.Delete(id);
        }
    }
}
