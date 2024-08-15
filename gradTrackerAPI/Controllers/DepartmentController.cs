using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// using gradTrackerAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using gradTrackerServices.Services;
using gradTrackerEntities.Entities;


namespace gradTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "Admin")]

    public class DepartmentController : ControllerBase
    {

        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Department>>> GetDepartments()
        {
            var departments = await _departmentService.GetDepartmentsAsync();
            if (departments == null || departments.Count == 0)
            {
                return NotFound();
            }
            return departments;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _departmentService.GetDepartmentAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return department;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            var result = await _departmentService.UpdateDepartmentAsync(department);
            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            var createdDepartment = await _departmentService.CreateDepartmentAsync(department);
            if (createdDepartment == null)
            {
                return Problem("Failed to create department.");
            }

            return CreatedAtAction(nameof(GetDepartment), new { id = createdDepartment.Id }, createdDepartment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result = await _departmentService.DeleteDepartmentAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // // private readonly GradTrackerContext _context;
        // private readonly IDepartmentService _departmentService;

        // public DepartmentController( IDepartmentService departmentService)
        // {
        //     // _context = context;
        //     _departmentService = departmentService;
        // }

        // // GET: api/Department
        // [HttpGet]
        // public async Task<ActionResult<List<gradTrackerEntities.Entities.Department>>> GetDepartments()
        // {
        //   if (_departmentService.GetDepartmentsAsync == null)
        //   {
        //       return NotFound();
        //   }
        //     return await _departmentService.GetDepartmentsAsync();
        // }

        // // GET: api/Department/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<Department>> GetDepartment(int id)
        // {
        //   if (_context.Departments == null)
        //   {
        //       return NotFound();
        //   }
        //     var department = await _context.Departments.FindAsync(id);

        //     if (department == null)
        //     {
        //         return NotFound();
        //     }

        //     return department;
        // }

        // // PUT: api/Department/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutDepartment(int id, Department department)
        // {
        //     if (id != department.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(department).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!DepartmentExists(id))
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

        // // POST: api/Department
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<Department>> PostDepartment(Department department)
        // {
        //   if (_context.Departments == null)
        //   {
        //       return Problem("Entity set 'GradTrackerContext.Departments'  is null.");
        //   }
        //     _context.Departments.Add(department);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetDepartment", new { id = department.Id }, department);
        // }

        // // DELETE: api/Department/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteDepartment(int id)
        // {
        //     if (_context.Departments == null)
        //     {
        //         return NotFound();
        //     }
        //     var department = await _context.Departments.FindAsync(id);
        //     if (department == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Departments.Remove(department);
        //     await _context.SaveChangesAsync();


        //     return NoContent();
        // }

        // private bool DepartmentExists(int id)
        // {
        //     return (_context.Departments?.Any(e => e.Id == id)).GetValueOrDefault();
        // }
    }
}
