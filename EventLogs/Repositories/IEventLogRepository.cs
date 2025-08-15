using EventLogs.Models;

namespace EventLogs.Repositories
{
    
        public interface IEventLogRepository
        {
        Task AddAsync(EventLog log);
        Task<IEnumerable<EventLog>> GetAllAsync();

        Task<IEnumerable<EventLog>> GetAsync(string eventType, DateTime? startDate, DateTime? endDate);
            Task SaveChangesAsync();
        }
    
}
