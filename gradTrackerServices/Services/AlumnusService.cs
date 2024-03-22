using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gradTrackerEntities.Entities;
using gradTrackerRepo.Repositories;

namespace gradTrackerServices.Services
{
    public interface IAlumnusService
    {
        Task<List<Alumnus>> GetAlumniAsync();
        Task<Alumnus> GetAlumnusAsync(int id);
        Task<Alumnus> CreateAlumnusAsync(Alumnus alumnus);
        Task<bool> UpdateAlumnusAsync(Alumnus alumnus);
        Task<bool> DeleteAlumnusAsync(int id);
    }

    public class AlumnusService : IAlumnusService
    {
        private readonly IAlumnusRepository _alumnusRepository;

        public AlumnusService(IAlumnusRepository alumnusRepository)
        {
            _alumnusRepository = alumnusRepository;
        }

        public async Task<List<Alumnus>> GetAlumniAsync()
        {
            return await _alumnusRepository.GetAlumniAsync();
        }

        public async Task<Alumnus> GetAlumnusAsync(int id)
        {
            return await _alumnusRepository.GetAlumnusAsync(id);
        }

        public async Task<Alumnus> CreateAlumnusAsync(Alumnus alumnus)
        {
            return await _alumnusRepository.CreateAlumnusAsync(alumnus);
        }

        public async Task<bool> UpdateAlumnusAsync(Alumnus alumnus)
        {
            return await _alumnusRepository.UpdateAlumnusAsync(alumnus);
        }

        public async Task<bool> DeleteAlumnusAsync(int id)
        {
            return await _alumnusRepository.DeleteAlumnusAsync(id);
        }
    }

}