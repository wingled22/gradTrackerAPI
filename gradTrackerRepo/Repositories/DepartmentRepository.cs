using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gradTrackerEntities.Entities;

namespace gradTrackerRepo.Repositories
{

    public interface IDepartmentRepository{
        List<Department> GetDepartments();
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly GradTrackerContext _context = new GradTrackerContext();
        public DepartmentRepository(GradTrackerContext context)
        {
            _context = context;
        }
        public List<Department> GetDepartments()
        {
    
            return _context.Departments.ToList();
         
        }
    }
}