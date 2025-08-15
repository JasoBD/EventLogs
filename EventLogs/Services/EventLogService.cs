using EventLogs.DTOs;
using EventLogs.Models;
using EventLogs.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EventLogs.Services
{
    public class EventLogService
    {
        private readonly IEventLogRepository _repository;

        public EventLogService(IEventLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<EventLog> CreateAsync(EventLogCreateDto dto)
        {
            try
            {
                if (dto == null)
                    throw new ArgumentNullException(nameof(dto), "El objeto de creación no puede ser nulo.");

                if (string.IsNullOrWhiteSpace(dto.EventType))
                    throw new ArgumentException("El tipo de evento es obligatorio.", nameof(dto.EventType));

                var newLog = new EventLog
                {
                    EventDate = dto.EventDate,
                    Description = dto.Description,
                    EventType = dto.EventType
                };

                await _repository.AddAsync(newLog);
                await _repository.SaveChangesAsync();

                return newLog;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Ocurrió un error al guardar el evento en la base de datos.", dbEx);
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Error de conexión con la base de datos SQL Server.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error inesperado al crear el evento: {ex.Message}", ex);
            }
        }

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
