using Healthcare.Data;
using Healthcare.DTOs;
using Healthcare.Interfaces;
using Healthcare.Models;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Services
{
    public class PatientService : IPatientService
    {
        private readonly AppDbContext _context;

        public PatientService(AppDbContext context)
        {
            _context = context;
        }

        public User AddPatient(UserDtos.UserDto dto)
        {
            var patient = new User
            {
                Name = dto.Name,
                Email = dto.Email
            };

            _context.Users.Add(patient);
            _context.SaveChanges();

            return patient;
        }

        public List<User> GetAllPatients()
        {
            return _context.Users.ToList();
        }

        public User GetPatientById(int id)
        {
            return _context.Users.FirstOrDefault(p => p.Id == id);
        }
    }
}
