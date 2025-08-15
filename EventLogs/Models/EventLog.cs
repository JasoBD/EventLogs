namespace EventLogs.Models
{
    public class EventLog
    {
        public int Id { get; set; }
        public DateTime EventDate { get; set; } 
        public string Description { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
