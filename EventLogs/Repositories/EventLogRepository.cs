using EventLogs.Data;
using EventLogs.Models;
using Microsoft.EntityFrameworkCore;

namespace EventLogs.Repositories
{
    public class EventLogRepository : IEventLogRepository
    {
        private readonly AppDbContext _context;

        public EventLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EventLog log)
        {
            try
            {
                await _context.EventLogs.AddAsync(log);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el evento en la base de datos (EventLogRepository.AddAsync)", ex);
            }
        }

        public async Task<IEnumerable<EventLog>> GetAsync(string eventType, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var query = _context.EventLogs.AsQueryable();

                if (!string.IsNullOrEmpty(eventType))
                    query = query.Where(e => e.EventType == eventType);

                if (startDate.HasValue)
                    query = query.Where(e => e.EventDate >= startDate.Value);

                if (endDate.HasValue)
                    query = query.Where(e => e.EventDate <= endDate.Value);

                return await query.OrderByDescending(e => e.EventDate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar los eventos en la base de datos (EventLogRepository.GetAsync)", ex);
            }
        }

        public async Task<IEnumerable<EventLog>> GetAllAsync()
        {
            try
            {
                return await _context.EventLogs
                .OrderByDescending(e => e.EventDate)
                .ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar los eventos en la base de datos (EventLogRepository.GetAsync)", ex);
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al guardar los cambios en la base de datos (EventLogRepository.SaveChangesAsync)", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al guardar los cambios (EventLogRepository.SaveChangesAsync)", ex);
            }
        }
    }
}
