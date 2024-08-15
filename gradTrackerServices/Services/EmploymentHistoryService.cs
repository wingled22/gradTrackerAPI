using System.Collections.Generic;
using System.Threading.Tasks;
using gradTrackerEntities.Entities;
using gradTrackerRepo.Repositories;

namespace gradTrackerServices.Services
{
  public interface IEmploymentHistoryService
  {
    Task<IEnumerable<EmploymentHistory>> GetEmploymentHistoriesByAlumniIdAsync(int alumniId);
    Task<EmploymentHistory> GetEmploymentHistoryByIdAsync(int id);
    Task<EmploymentHistory> AddEmploymentHistoryAsync(EmploymentHistory employmentHistory);
    Task<EmploymentHistory> UpdateEmploymentHistoryAsync(EmploymentHistory employmentHistory);
    Task<bool> DeleteEmploymentHistoryAsync(int id);
    Task<bool> EmploymentHistoryExistsAsync(int id);
  }
  public class EmploymentHistoryService : IEmploymentHistoryService
    {
        private readonly IEmploymentHistoryRepository _repository;

        public EmploymentHistoryService(IEmploymentHistoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmploymentHistory>> GetEmploymentHistoriesByAlumniIdAsync(int alumniId)
        {
            // Here, you can add any business logic if needed before interacting with the repository.
            return await _repository.GetEmploymentHistoriesByAlumniIdAsync(alumniId);
        }

        public async Task<EmploymentHistory> GetEmploymentHistoryByIdAsync(int id)
        {
            // Additional business logic can be applied here.
            return await _repository.GetEmploymentHistoryByIdAsync(id);
        }

        public async Task<EmploymentHistory> AddEmploymentHistoryAsync(EmploymentHistory employmentHistory)
        {
            // You can add validation or other business rules here before adding the record.
            return await _repository.AddEmploymentHistoryAsync(employmentHistory);
        }

        public async Task<EmploymentHistory> UpdateEmploymentHistoryAsync(EmploymentHistory employmentHistory)
        {
            // Business rules or validation can be applied here before updating the record.
            return await _repository.UpdateEmploymentHistoryAsync(employmentHistory);
        }

        public async Task<bool> DeleteEmploymentHistoryAsync(int id)
        {
            // Add any business rules here before deleting the record.
            return await _repository.DeleteEmploymentHistoryAsync(id);
        }

        public async Task<bool> EmploymentHistoryExistsAsync(int id)
        {
            // Any necessary checks or additional logic can be applied here.
            return await _repository.EmploymentHistoryExistsAsync(id);
        }
    }
}
