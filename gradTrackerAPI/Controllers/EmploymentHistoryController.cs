using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using gradTrackerEntities.Entities;
using gradTrackerServices.Services;

namespace gradTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmploymentHistoryController : ControllerBase
    {
        private readonly IEmploymentHistoryService _service;

        public EmploymentHistoryController(IEmploymentHistoryService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<EmploymentHistory>>> GetEmploymentHistory(int id)
        {
            var employmentHistories = await _service.GetEmploymentHistoriesByAlumniIdAsync(id);
            if (employmentHistories == null || !employmentHistories.Any())
            {
                return NotFound();
            }

            return Ok(employmentHistories);
        }

        [HttpPost]
        public async Task<ActionResult<EmploymentHistory>> PostEmploymentHistory(EmploymentHistory employmentHistory)
        {
            try
            {
                var createdEmploymentHistory = await _service.AddEmploymentHistoryAsync(employmentHistory);
                return CreatedAtAction(nameof(GetEmploymentHistory), new { id = createdEmploymentHistory.Id }, createdEmploymentHistory);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmploymentHistory(int id, EmploymentHistory employmentHistory)
        {
            if (id != employmentHistory.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedEmploymentHistory = await _service.UpdateEmploymentHistoryAsync(employmentHistory);
                if (updatedEmploymentHistory == null)
                {
                    return NotFound();
                }
            }
            catch
            {
                if (!await _service.EmploymentHistoryExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmploymentHistory(int id)
        {
            var deleted = await _service.DeleteEmploymentHistoryAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
