using EventLogs.DTOs;
using EventLogs.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventLogs.Controllers
{
    [ApiController] // Indica que este controlador es un API controller
    [Route("api/[controller]")] // La ruta base será: /api/EventLogs
    public class EventLogsController : ControllerBase
    {
        private readonly EventLogService _service;

        // Constructor: inyecta el servicio que maneja la lógica de negocio
        public EventLogsController(EventLogService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene todos los registros de eventos sin filtro.
        /// GET: /api/EventLogs
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var logs = await _service.GetAllAsync(); // Llama al servicio para traer todos los logs
                return Ok(logs); // Retorna 200 OK con la lista de logs
            }
            catch (Exception ex)
            {
                // Manejo de excepciones: retorna 500 con mensaje de error
                return StatusCode(500, new { mensaje = ex.Message, detalle = ex.InnerException?.Message });
            }
        }

        /// <summary>
        /// Crea un nuevo registro de evento.
        /// POST: /api/EventLogs
        /// </summary>
        /// <param name="dto">Objeto DTO con la información del evento</param>
        [HttpPost]
        public async Task<IActionResult> Create(EventLogCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Retorna 400 si el modelo no es válido

            try
            {
                var log = await _service.CreateAsync(dto); // Llama al servicio para crear el evento
                return Ok(log); // Retorna 200 OK con el evento creado
            }
            catch (Exception ex)
            {
                // Manejo de excepciones: retorna 500 con mensaje de error
                return StatusCode(500, new { mensaje = ex.Message, detalle = ex.InnerException?.Message });
            }
        }

        /// <summary>
        /// Obtiene eventos filtrados por tipo y/o rango de fechas.
        /// GET: /api/EventLogs/filter?eventType=API&startDate=2025-08-01&endDate=2025-08-15
        /// </summary>
        /// <param name="eventType">Tipo de evento: "API" o "Manual"</param>
        /// <param name="startDate">Fecha de inicio del filtro (opcional)</param>
        /// <param name="endDate">Fecha de fin del filtro (opcional)</param>
        [HttpGet("filter")]
        public async Task<IActionResult> Get([FromQuery] string eventType, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                // Llama al servicio para obtener logs filtrados según los parámetros
                var logs = await _service.GetAsync(eventType, startDate, endDate);
                return Ok(logs); // Retorna 200 OK con la lista filtrada
            }
            catch (Exception ex)
            {
                // Manejo de excepciones: retorna 500 con mensaje de error
                return StatusCode(500, new { mensaje = ex.Message, detalle = ex.InnerException?.Message });
            }
        }
    }
}
