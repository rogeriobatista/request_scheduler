namespace request_scheduler.Domain.Schedule.Dtos
{
    public class ScheduleDto
    {
        public string Id { get; set; }
        public string CronExpression { get; set; }
        public int PackageSize { get; set; }
    }
}
