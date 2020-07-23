using Hangfire;
using Microsoft.AspNetCore.Mvc;
using request_scheduler.Domain.MauticForms.Interfaces;
using request_scheduler.Domain.Schedule.Dtos;

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
            _recurringJobManager.AddOrUpdate(schedule.Id, () => _mauticFormService.Enqueue(schedule.Id, schedule.PackageSize), schedule.CronExpression);
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
