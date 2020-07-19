using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using request_scheduler.Domain.MauticForms.Enums;
using request_scheduler.Domain.MauticForms.Interfaces;
using request_scheduler.Domain.Schedule.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace request_scheduler.Controllers
{
    [Route("api/[controller]")]
    public class ScheduleController : Controller
    {
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IMauticFormService _mauticFormService;

        public ScheduleController(IRecurringJobManager recurringJobManager, IMauticFormService mauticFormService)
        {
            _recurringJobManager = recurringJobManager;
            _mauticFormService = mauticFormService;
        }

        [HttpPost]
        public void Post([FromBody] ScheduleDto schedule)
        {
            _recurringJobManager.AddOrUpdate(schedule.Id, () => _mauticFormService.Enqueue(MauticFormSendFrequency.Minutely, schedule.PackageSize), Cron.MinuteInterval(schedule.Frequency));
        }

        [HttpGet("{id}/trigger")]
        public void Trigger(string id)
        {
            _recurringJobManager.Trigger(id);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _recurringJobManager.RemoveIfExists(id);
        }
    }
}
