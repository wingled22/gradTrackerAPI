using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using gradTrackerAPI.Entities;
using gradTrackerAPI.ViewModel;


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
            //var employmentHistory = await _context.EmploymentHistories.FindAsync(id);
            var employmentHistory = 
            (
                from alum in _context.Alumni
                join employment in _context.EmploymentHistories
                on alum.Id equals employment.AlumniId
                where id == employment.AlumniId

                select new AlumniEmployment
                {
                    AlumniId = alum.Id,
                    Id = employment.Id,
                    CompanyName = employment.CompanyName,
                    Position = employment.Position,
                    StartDate = employment.StartDate,
                    EndDate = employment.EndDate,
                }

            ).ToList();

            if (employmentHistory == null)
            {
                return NotFound();
            }

            return Ok(employmentHistory);
        }

        [HttpPost]
        public async Task<ActionResult<EmploymentHistory>> PostEmploymentHistory(EmploymentHistory employmentHistory)
        {
            if (_context.EmploymentHistories == null)
            {
                return Problem("Entity set 'GradTrackerContext.Alumni'  is null.");
            }
            _context.EmploymentHistories.Add(employmentHistory);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetAlumnus", new { id = alumnus.Id }, alumnus);
            return Ok();
        }
    }
}