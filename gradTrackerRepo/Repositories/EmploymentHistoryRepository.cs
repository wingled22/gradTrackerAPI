using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gradTrackerEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace gradTrackerRepo.Repositories
{
  public interface IEmploymentHistoryRepository
  {
    Task<IEnumerable<EmploymentHistory>> GetEmploymentHistoriesByAlumniIdAsync(int alumniId);
    Task<EmploymentHistory> GetEmploymentHistoryByIdAsync(int id);
    Task<EmploymentHistory> AddEmploymentHistoryAsync(EmploymentHistory employmentHistory);
    Task<EmploymentHistory> UpdateEmploymentHistoryAsync(EmploymentHistory employmentHistory);
    Task<bool> DeleteEmploymentHistoryAsync(int id);
    Task<bool> EmploymentHistoryExistsAsync(int id);
  }
  public class EmploymentHistoryRepository : IEmploymentHistoryRepository
  {
    private readonly GradTrackerContext _context;

    public EmploymentHistoryRepository(GradTrackerContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EmploymentHistory>> GetEmploymentHistoriesByAlumniIdAsync(int alumniId)
    {
      return await _context.EmploymentHistories
        .Where(e => e.AlumniId == alumniId)
        .OrderByDescending(e => e.Id)
        .ToListAsync();
    }

    public async Task<EmploymentHistory> GetEmploymentHistoryByIdAsync(int id)
    {
      return await _context.EmploymentHistories.FindAsync(id);
    }

    public async Task<EmploymentHistory> AddEmploymentHistoryAsync(EmploymentHistory employmentHistory)
    {
      _context.EmploymentHistories.Add(employmentHistory);
      await _context.SaveChangesAsync();
      return employmentHistory;
    }

    public async Task<EmploymentHistory> UpdateEmploymentHistoryAsync(EmploymentHistory employmentHistory)
    {
      _context.Entry(employmentHistory).State = EntityState.Modified;
      await _context.SaveChangesAsync();
      return employmentHistory;
    }

    public async Task<bool> DeleteEmploymentHistoryAsync(int id)
    {
      var employmentHistory = await _context.EmploymentHistories.FindAsync(id);
      if (employmentHistory == null)
      {
        return false;
      }

      _context.EmploymentHistories.Remove(employmentHistory);
      await _context.SaveChangesAsync();
      return true;
    }
    public async Task<bool> EmploymentHistoryExistsAsync(int id)
    {
      return await _context.EmploymentHistories.AnyAsync(e => e.Id == id);
    }
  }
}
