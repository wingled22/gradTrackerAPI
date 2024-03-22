using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using gradTrackerAPI.Entities;
using gradTrackerAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace gradTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin")]
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
            var employmentHistory = await _context.EmploymentHistories.Where(e => e.AlumniId == id).OrderByDescending(e => e.Id).ToListAsync();

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
            catch (Exception)
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

            _context.Entry(employmentHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmploymentHistoryExists(id))
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

        private bool EmploymentHistoryExists(int id)
        {
            return (_context.EmploymentHistories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}