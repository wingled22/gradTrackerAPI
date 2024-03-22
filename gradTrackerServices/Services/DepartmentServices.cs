using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gradTrackerEntities.Entities;
using gradTrackerRepo.Repositories;

namespace gradTrackerServices.Services
{
    public interface IDepartmentService{
        List<Department> GetDepartments();
    }

    public class DepartmentServices : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentServices(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public List<Department> GetDepartments()
        {
            return _departmentRepository.GetDepartments();
        }
    }
}