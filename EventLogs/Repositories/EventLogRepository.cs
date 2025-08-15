using EventLogs.Data;
using EventLogs.Models;
using Microsoft.EntityFrameworkCore;

namespace EventLogs.Repositories
{
    /// <summary>
    /// Repositorio que maneja el acceso a la base de datos para los registros de eventos.
    /// Encapsula las operaciones CRUD y consultas filtradas.
    /// </summary>
    public class EventLogRepository : IEventLogRepository
    {
        private readonly AppDbContext _context;

        // Constructor: inyecta el contexto de base de datos
        public EventLogRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Agrega un nuevo evento al contexto de EF Core.
        /// </summary>
        /// <param name="log">Objeto EventLog a insertar</param>
        public async Task AddAsync(EventLog log)
        {
            try
            {
                await _context.EventLogs.AddAsync(log); // Agrega el evento al DbSet
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el evento en la base de datos (EventLogRepository.AddAsync)", ex);
            }
        }

        /// <summary>
        /// Obtiene eventos filtrados por tipo y rango de fechas.
        /// </summary>
        /// <param name="eventType">Tipo de evento: "API" o "Manual"</param>
        /// <param name="startDate">Fecha de inicio del filtro (opcional)</param>
        /// <param name="endDate">Fecha de fin del filtro (opcional)</param>
        /// <returns>Lista de eventos filtrados</returns>
        public async Task<IEnumerable<EventLog>> GetAsync(string eventType, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var query = _context.EventLogs.AsQueryable(); // Consulta base

                // Filtra por tipo de evento si se especifica
                if (!string.IsNullOrEmpty(eventType))
                    query = query.Where(e => e.EventType == eventType);

                // Filtra por fecha de inicio si se especifica
                if (startDate.HasValue)
                    query = query.Where(e => e.EventDate >= startDate.Value);

                // Filtra por fecha de fin si se especifica
                if (endDate.HasValue)
                    query = query.Where(e => e.EventDate <= endDate.Value);

                // Retorna los eventos ordenados de más recientes a más antiguos
                return await query.OrderByDescending(e => e.EventDate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar los eventos en la base de datos (EventLogRepository.GetAsync)", ex);
            }
        }

        /// <summary>
        /// Obtiene todos los eventos registrados en la base de datos.
        /// </summary>
        /// <returns>Lista de todos los eventos</returns>
        public async Task<IEnumerable<EventLog>> GetAllAsync()
        {
            try
            {
                return await _context.EventLogs
                    .OrderByDescending(e => e.EventDate) // Ordena de más reciente a más antiguo
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar los eventos en la base de datos (EventLogRepository.GetAllAsync)", ex);
            }
        }

        /// <summary>
        /// Guarda los cambios pendientes en la base de datos.
        /// </summary>
        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync(); // Persiste los cambios en la DB
            }
            catch (DbUpdateException dbEx)
            {
                // Captura errores específicos de EF Core al actualizar la base
                throw new Exception("Error al guardar los cambios en la base de datos (EventLogRepository.SaveChangesAsync)", dbEx);
            }
            catch (Exception ex)
            {
                // Captura cualquier error inesperado
                throw new Exception("Error inesperado al guardar los cambios (EventLogRepository.SaveChangesAsync)", ex);
            }
        }
    }
}
