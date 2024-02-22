using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gradTrackerAPI.Entities;

namespace gradTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumniController : ControllerBase
    {
        private readonly GradTrackerContext _context;

        public AlumniController(GradTrackerContext context)
        {
            _context = context;
        }

        // GET: api/Alumnus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumnus>>> GetDepartments()
        {
          if (_context.Alumni == null)
          {
              return NotFound();
          }
            return await _context.Alumni.ToListAsync();
        }

        // GET: api/Alumnus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alumnus>> GetAlumnus(int id)
        {
          if (_context.Alumni == null)
          {
              return NotFound();
          }
            var alumnus = await _context.Alumni.FindAsync(id);

            if (alumnus == null)
            {
                return NotFound();
            }

            return alumnus;
        }

        // PUT: api/Alumnus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlumnus(int id, Alumnus alumnus)
        {
            if (id != alumnus.Id)
            {
                return BadRequest();
            }

            _context.Entry(alumnus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnusExists(id))
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

        // POST: api/Alumnus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Alumnus>> PostAlumnus(Alumnus alumnus)
        {
          if (_context.Alumni == null)
          {
            return Problem("Entity set 'GradTrackerContext.Alumni'  is null.");
          }
            _context.Alumni.Add(alumnus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlumnus", new { id = alumnus.Id }, alumnus);
        }

         // DELETE: api/Alumnus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlumnus(int id)
        {
            if (_context.Alumni == null)
            {
                return NotFound();
            }
            var alumnus = await _context.Alumni.FindAsync(id);
            if (alumnus == null)
            {
                return NotFound();
            }

            _context.Alumni.Remove(alumnus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlumnusExists(int id)
        {
            return (_context.Alumni?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}