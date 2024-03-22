using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gradTrackerEntities.Entities;
using gradTrackerRepo.Repositories;

namespace gradTrackerServices.Services
{

    public interface IDepartmentService
{
    Task<List<Department>> GetDepartmentsAsync();
    Task<Department> GetDepartmentAsync(int id);
    Task<Department> CreateDepartmentAsync(Department department);
    Task<bool> UpdateDepartmentAsync(Department department);
    Task<bool> DeleteDepartmentAsync(int id);
}

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<List<Department>> GetDepartmentsAsync()
    {
        return await _departmentRepository.GetDepartmentsAsync();
    }

    public async Task<Department> GetDepartmentAsync(int id)
    {
        return await _departmentRepository.GetDepartmentAsync(id);
    }

    public async Task<Department> CreateDepartmentAsync(Department department)
    {
        return await _departmentRepository.CreateDepartmentAsync(department);
    }

    public async Task<bool> UpdateDepartmentAsync(Department department)
    {
        return await _departmentRepository.UpdateDepartmentAsync(department);
    }

    public async Task<bool> DeleteDepartmentAsync(int id)
    {
        return await _departmentRepository.DeleteDepartmentAsync(id);
    }
}




    // public interface IDepartmentService{
    //     Task<List<Department>> GetDepartmentsAsync();
    // }

    // public class DepartmentServices : IDepartmentService
    // {
    //     private readonly IDepartmentRepository _departmentRepository;
    //     public DepartmentServices(IDepartmentRepository departmentRepository)
    //     {
    //         _departmentRepository = departmentRepository;
    //     }

    //     public async Task<List<Department>> GetDepartmentsAsync()
    //     {
    //         return await _departmentRepository.GetDepartmentsAsync();
    //     }
    // }
}