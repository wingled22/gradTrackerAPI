using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gradTrackerAPI.Entities;
using NuGet.Common;

namespace gradTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly GradTrackerContext _context;

        public RoleController(GradTrackerContext context)
        {
            _context = context;
        }

        // GET: api/Role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
            return await _context.Roles.ToListAsync();
        }

        // // GET: api/Role/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<Role>> GetRole(Guid id)
        // {
        //   if (_context.Roles == null)
        //   {
        //       return NotFound();
        //   }
        //     var role = await _context.Roles.FindAsync(id);

        //     if (role == null)
        //     {
        //         return NotFound();
        //     }

        //     return role;
        // }

        // // PUT: api/Role/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutRole(Guid id, Role role)
        // {
        //     if (id != role.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(role).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!RoleExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // // POST: api/Role
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<Role>> PostRole(Role role)
        // {
        //   if (_context.Roles == null)
        //   {
        //       return Problem("Entity set 'GradTrackerContext.Roles'  is null.");
        //   }
        //     role.Id = Guid.NewGuid();
        //     role.ConcurrencyStamp = DateTime.UtcNow.ToString();
        //     _context.Roles.Add(role);
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateException)
        //     {
        //         if (RoleExists(role.Id))
        //         {
        //             return Conflict();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return CreatedAtAction("GetRole", new { id = role.Id }, role);
        // }

        // // DELETE: api/Role/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteRole(Guid id)
        // {
        //     if (_context.Roles == null)
        //     {
        //         return NotFound();
        //     }
        //     var role = await _context.Roles.FindAsync(id);
        //     if (role == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Roles.Remove(role);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // private bool RoleExists(Guid id)
        // {
        //     return (_context.Roles?.Any(e => e.Id == id)).GetValueOrDefault();
        // }
    }
}
