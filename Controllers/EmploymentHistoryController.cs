using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using gradTrackerAPI.Entities;
using gradTrackerAPI.ViewModel;
using Microsoft.EntityFrameworkCore;


namespace gradTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmploymentHistoryController : ControllerBase
    {
        private readonly GradTrackerContext _context;

        public EmploymentHistoryController(GradTrackerContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmploymentHistory>> GetEmploymentHistory(int id)
        {
            if (_context.EmploymentHistories == null)
            {
                return NotFound();
            }
            var employmentHistory = await _context.EmploymentHistories.Where(e => e.AlumniId == id).ToListAsync();

            if (employmentHistory == null)
            {
                return NotFound();
            }

            return Ok(employmentHistory);
        }

        [HttpPost]
        public async Task<ActionResult<EmploymentHistory>> PostEmploymentHistory(EmploymentHistory employmentHistory)
        {
            try
            {
                if (_context == null || _context.EmploymentHistories == null)
                {
                    return Problem("Context or entity set 'GradTrackerContext.EmploymentHistories' is null.");
                }

                _context.EmploymentHistories.Add(employmentHistory);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEmploymentHistory), new { id = employmentHistory.Id }, employmentHistory);
            }
            catch (Exception ex)
            {
            
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/EmploymentHistory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmploymentHistory(int id)
        {
            if (_context.EmploymentHistories == null)
            {
                return NotFound();
            }

            var employmentHistory = await _context.EmploymentHistories.FindAsync(id);
            if (employmentHistory == null)
            {
                return NotFound();
            }

            _context.EmploymentHistories.Remove(employmentHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}