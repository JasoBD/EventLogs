namespace EventLogs.DTOs
{
    public class EventLogCreateDto
    {
        public DateTime EventDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
    }
}
