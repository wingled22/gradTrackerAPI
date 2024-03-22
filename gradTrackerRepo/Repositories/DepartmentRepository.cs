using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gradTrackerEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace gradTrackerRepo.Repositories
{

    public interface IDepartmentRepository
    {
        Task<List<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartmentAsync(int id);
        Task<Department> CreateDepartmentAsync(Department department);
        Task<bool> UpdateDepartmentAsync(Department department);
        Task<bool> DeleteDepartmentAsync(int id);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly GradTrackerContext _context;

        public DepartmentRepository(GradTrackerContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<Department> CreateDepartmentAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<bool> UpdateDepartmentAsync(Department department)
        {
            _context.Entry(department).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(department.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return false;
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
    }

}