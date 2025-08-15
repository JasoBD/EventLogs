using EventLogs.Models;

namespace EventLogs.Repositories
{
    /// <summary>
    /// Interfaz que define las operaciones de acceso a datos para los registros de eventos.
    /// Permite desacoplar la lógica de negocio del acceso a la base de datos.
    /// </summary>
    public interface IEventLogRepository
    {
        /// <summary>
        /// Agrega un nuevo evento al contexto para ser guardado en la base de datos.
        /// </summary>
        /// <param name="log">Objeto EventLog a agregar</param>
        Task AddAsync(EventLog log);

        /// <summary>
        /// Obtiene todos los eventos registrados en la base de datos, ordenados de más reciente a más antiguo.
        /// </summary>
        /// <returns>Lista de todos los eventos</returns>
        Task<IEnumerable<EventLog>> GetAllAsync();

        /// <summary>
        /// Obtiene eventos filtrados por tipo y/o rango de fechas.
        /// </summary>
        /// <param name="eventType">Tipo de evento: "API" o "Manual"</param>
        /// <param name="startDate">Fecha de inicio del filtro (opcional)</param>
        /// <param name="endDate">Fecha de fin del filtro (opcional)</param>
        /// <returns>Lista de eventos filtrados</returns>
        Task<IEnumerable<EventLog>> GetAsync(string eventType, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// Persiste los cambios pendientes en la base de datos.
        /// </summary>
        Task SaveChangesAsync();
    }
}
