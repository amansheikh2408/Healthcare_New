//using Healthcare.DTOs;
//using Healthcare.Interfaces;
//using Healthcare.Models;

//namespace Healthcare.Services
//{
//    public class AppointmentService: IAppointmentService
//    {
//        private readonly IDoctorService _doctorService;
//        private readonly IPatientService _patientService;
//        private readonly List<Appointment> _appointments = new();
//        private int _appointmentIdCounter = 1;

//        public AppointmentService(IDoctorService doctorService, IPatientService patientService)
//        {
//            _doctorService = doctorService;
//            _patientService = patientService;
//        }

//        public Appointment BookAppointment(AppointmentDto dto)
//        {
//            var doctor = _doctorService.GetDoctorById(dto.DoctorId);
//            var patient = _patientService.GetPatientById(dto.UserId);

//            if (doctor == null || patient== null)
//                throw new Exception("Doctor or Patient not found");

//            // Find the slot matching date + start + end and not booked
//            var slot = doctor.Slots.FirstOrDefault(s =>
//                s.Date.Date == dto.Date.Date &&
//                s.StartTime == dto.StartTime &&
//                s.EndTime == dto.EndTime &&
//                !s.IsBooked
//            );

//            if (slot == null)
//                throw new Exception("Slot not available");

//            // Mark slot as booked
//            slot.IsBooked = true;
//            slot.UserId = patient.Id;

//            // Create appointment record
//            var appointment = new Appointment
//            {
//                Id = _appointmentIdCounter++,
//                DoctorId = doctor.Id,
//                UserId = patient.Id,
//                SlotTime = slot.Date + slot.StartTime // Optional: store DateTime for easy queries
//            };

//            patient.Appointments.Add(appointment);
//            _appointments.Add(appointment);

//            return appointment;
//        }

//        public List<Appointment> GetAppointmentsByPatient(int patientId)
//            => _appointments.Where(a => a.UserId == patientId).ToList();

//        public List<Appointment> GetAppointmentsByDoctor(int doctorId)
//            => _appointments.Where(a => a.DoctorId == doctorId).ToList();
//    }
//}
using Healthcare.Data;
using Healthcare.DTOs;
using Healthcare.DTOs.ResponseDtos;
using Healthcare.Interfaces;
using Healthcare.Models;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Services
{

    public class AppointmentService : IAppointmentService
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly AppDbContext _context;

        public AppointmentService(IDoctorService doctorService, IPatientService patientService, AppDbContext context)
        {
            _doctorService = doctorService;
            _patientService = patientService;
            _context = context;
        }

        public Appointment BookAppointment(AppointmentDto dto)
        {
            var doctor = _doctorService.GetDoctorById(dto.DoctorId);
            var patient = _patientService.GetPatientById(dto.UserId);

            if (doctor == null || patient == null)
                throw new Exception("Doctor or Patient not found");

            // Find the slot matching date + start + end and not booked
            var slot = doctor.Slots.FirstOrDefault(s =>
                s.Date.Date == dto.Date.Date &&
                s.StartTime == dto.StartTime &&
                s.EndTime == dto.EndTime &&
                !s.IsBooked
            );

            if (slot == null)
                throw new Exception("Slot not available");

            // Mark slot as booked
            slot.IsBooked = true;
            slot.UserId = patient.Id;

            // Update slot in database
            _context.DoctorSlots.Update(slot);

            // Create appointment record
            var appointment = new Appointment
            {
                DoctorId = doctor.Id,
                UserId = patient.Id,
                SlotTime = slot.Date + slot.StartTime
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return appointment;
        }

        public List<AppointmentResponseDto> GetAppointmentsByPatient(int patientId)
        {
            return _context.Appointments
                .Where(a => a.UserId == patientId)
                .Include(a => a.Doctor)
                .Include(a => a.User)
                .Select(a => new AppointmentResponseDto
                {
                    Id = a.Id,
                    SlotTime = a.SlotTime,
                    Doctor = new DoctorInfoDto
                    {
                        Id = a.Doctor.Id,
                        Name = a.Doctor.Name,
                        Email = a.Doctor.Email,
                        Specialization = a.Doctor.Specialization
                    },
                    User = new UserInfoDto
                    {
                        Id = a.User.Id,
                        Name = a.User.Name,
                        Email = a.User.Email
                    }
                })
                .ToList();
        }

        public List<AppointmentResponseDto> GetAppointmentsByDoctor(int doctorId)
        {
            return _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Include(a => a.Doctor)
                .Include(a => a.User)
                .Select(a => new AppointmentResponseDto
                {
                    Id = a.Id,
                    SlotTime = a.SlotTime,
                    Doctor = new DoctorInfoDto
                    {
                        Id = a.Doctor.Id,
                        Name = a.Doctor.Name,
                        Email = a.Doctor.Email,
                        Specialization = a.Doctor.Specialization
                    },
                    User = new UserInfoDto
                    {
                        Id = a.User.Id,
                        Name = a.User.Name,
                        Email = a.User.Email
                    }
                })
                .ToList();
        }


    }
}
