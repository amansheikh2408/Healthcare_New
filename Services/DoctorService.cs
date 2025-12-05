using Healthcare.Data;
using Healthcare.DTOs;
using Healthcare.Interfaces;
using Healthcare.Models;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly AppDbContext _context;

        public DoctorService(AppDbContext context)
        {
            _context = context;
        }

        public Doctor AddDoctor(DoctorDto dto)
        {
            var doctor = new Doctor
            {
                Name = dto.Name,
                Email = dto.Email,
                Specialization = dto.Specialization
            };

            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            return doctor;
        }

        public List<Doctor> GetAllDoctors()
        {
            return _context.Doctors
                .Include(d => d.Slots)
                .ToList();
        }

        public Doctor GetDoctorById(int id)
        {
            return _context.Doctors
                .Include(d => d.Slots) // Include slots for booking
                .FirstOrDefault(d => d.Id == id);
        }

        public void AddDoctorSlot(int doctorId, DoctorSlotDto slotDto)
        {
            var doctor = GetDoctorById(doctorId);
            if (doctor == null)
                throw new Exception("Doctor not found");

            var slot = new DoctorSlot
            {
                Date = slotDto.Date,
                StartTime = slotDto.StartTime,
                EndTime = slotDto.EndTime,
                IsBooked = false,
                DoctorId = doctor.Id
            };

            _context.DoctorSlots.Add(slot);
            _context.SaveChanges();
        }
    }
}
