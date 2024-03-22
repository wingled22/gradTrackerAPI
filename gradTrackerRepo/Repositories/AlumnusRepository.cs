using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gradTrackerEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace gradTrackerRepo.Repositories
{
    public interface IAlumnusRepository
    {
        Task<List<Alumnus>> GetAlumniAsync();
        Task<Alumnus> GetAlumnusAsync(int id);
        Task<Alumnus> CreateAlumnusAsync(Alumnus alumnus);
        Task<bool> UpdateAlumnusAsync(Alumnus alumnus);
        Task<bool> DeleteAlumnusAsync(int id);
    }

    public class AlumnusRepository : IAlumnusRepository
    {
        private readonly GradTrackerContext _context;

        public AlumnusRepository(GradTrackerContext context)
        {
            _context = context;
        }

        public async Task<List<Alumnus>> GetAlumniAsync()
        {
            return await _context.Alumni.OrderByDescending(a => a.Id).ToListAsync();
        }

        public async Task<Alumnus> GetAlumnusAsync(int id)
        {
            return await _context.Alumni.FindAsync(id);
        }

        public async Task<Alumnus> CreateAlumnusAsync(Alumnus alumnus)
        {
            _context.Alumni.Add(alumnus);
            await _context.SaveChangesAsync();
            return alumnus;
        }

        public async Task<bool> UpdateAlumnusAsync(Alumnus alumnus)
        {
            _context.Entry(alumnus).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnusExists(alumnus.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAlumnusAsync(int id)
        {
            var alumnus = await _context.Alumni.FindAsync(id);
            if (alumnus == null)
            {
                return false;
            }

            _context.Alumni.Remove(alumnus);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool AlumnusExists(int id)
        {
            return _context.Alumni.Any(e => e.Id == id);
        }
    }

}