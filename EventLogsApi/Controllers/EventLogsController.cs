using EventLogs.DTOs;
using EventLogs.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventLogs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventLogsController : ControllerBase
    {
        private readonly EventLogService _service;

        public EventLogsController(EventLogService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var logs = await _service.GetAllAsync();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex.Message, detalle = ex.InnerException?.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(EventLogCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var log = await _service.CreateAsync(dto);
                return Ok(log);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex.Message, detalle = ex.InnerException?.Message });
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Get([FromQuery] string eventType, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                var logs = await _service.GetAsync(eventType, startDate, endDate);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex.Message, detalle = ex.InnerException?.Message });
            }
        }

       
    }
}
