namespace request_scheduler.Domain.Schedule.Dtos
{
    public class ScheduleDto
    {
        public string Id { get; set; }
        public int Frequency { get; set; }
        public int PackageSize { get; set; }
    }
}
