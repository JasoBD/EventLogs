using EventLogs.DTOs;
using EventLogs.Models;
using EventLogs.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EventLogs.Services
{
    /// <summary>
    /// Servicio que maneja la lógica de negocio de los registros de eventos.
    /// Encapsula operaciones de creación y consulta de eventos.
    /// </summary>
    public class EventLogService
    {
        private readonly IEventLogRepository _repository;

        // Constructor: inyecta el repositorio para interactuar con la base de datos
        public EventLogService(IEventLogRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Crea un nuevo evento en la base de datos.
        /// </summary>
        /// <param name="dto">Objeto DTO con la información del evento</param>
        /// <returns>Evento creado</returns>
        public async Task<EventLog> CreateAsync(EventLogCreateDto dto)
        {
            try
            {
                // Validaciones básicas
                if (dto == null)
                    throw new ArgumentNullException(nameof(dto), "El objeto de creación no puede ser nulo.");

                if (string.IsNullOrWhiteSpace(dto.EventType))
                    throw new ArgumentException("El tipo de evento es obligatorio.", nameof(dto.EventType));

                // Crear el objeto EventLog con los datos del DTO
                var newLog = new EventLog
                {
                    EventDate = dto.EventDate,
                    Description = dto.Description,
                    EventType = dto.EventType
                };

                // Guardar en la base de datos usando el repositorio
                await _repository.AddAsync(newLog);
                await _repository.SaveChangesAsync();

                return newLog; // Retorna el evento creado
            }
            catch (DbUpdateException dbEx)
            {
                // Captura errores de actualización de Entity Framework
                throw new Exception("Ocurrió un error al guardar el evento en la base de datos.", dbEx);
            }
            catch (SqlException sqlEx)
            {
                // Captura errores de conexión con SQL Server
                throw new Exception("Error de conexión con la base de datos SQL Server.", sqlEx);
            }
            catch (Exception ex)
            {
                // Captura cualquier otro error inesperado
                throw new Exception($"Ocurrió un error inesperado al crear el evento: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene eventos filtrados por tipo y/o rango de fechas.
        /// </summary>
        /// <param name="eventType">Tipo de evento: "API" o "Manual"</param>
        /// <param name="startDate">Fecha de inicio del filtro (opcional)</param>
        /// <param name="endDate">Fecha de fin del filtro (opcional)</param>
        /// <returns>Lista de eventos filtrados</returns>
        public async Task<IEnumerable<EventLog>> GetAsync(string eventType, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                return await _repository.GetAsync(eventType, startDate, endDate);
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Error al consultar la base de datos SQL Server.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error inesperado al obtener los eventos: {ex.Message}", ex);
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
                return await _repository.GetAllAsync();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Error al consultar la base de datos SQL Server.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error inesperado al obtener los eventos: {ex.Message}", ex);
            }
        }
    }
}
