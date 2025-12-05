using Healthcare.DTOs;
using Healthcare.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Healthcare.DTOs.UserDtos;

namespace Healthcare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;

        public UserController(IDoctorService doctorService, IPatientService patientService, IAppointmentService appointmentService)
        {
            _doctorService = doctorService;
            _patientService = patientService;
            _appointmentService = appointmentService;
        }

        [HttpGet("doctors")]
        public IActionResult GetAllDoctors()
        {
            var doctors = _doctorService.GetAllDoctors()
                .Select(d => new DoctorDtoResponse
                {
                    Id = d.Id,
                    Name = d.Name,
                    Email = d.Email,
                    Specialization = d.Specialization,
                    Slots = d.Slots.Select(s => new DoctorSlotDtoResponse
                    {
                        Id = s.Id,
                        Date = s.Date,
                        StartTime = s.StartTime,
                        EndTime = s.EndTime,
                        IsBooked = s.IsBooked,
                        UserId = s.UserId
                    }).ToList()
                }).ToList();

            return Ok(doctors);
        }

        [HttpPost("add-patient")]
        public IActionResult AddPatient([FromBody] UserDto dto) => Ok(_patientService.AddPatient(dto));

        [HttpPost("book-appointment")]
        public IActionResult BookAppointment([FromBody] AppointmentDto dto)
        {
            try
            {
                return Ok(_appointmentService.BookAppointment(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("my-appointments/{patientId}")]
        public IActionResult MyAppointments(int patientId)
            => Ok(_appointmentService.GetAppointmentsByPatient(patientId));
    }
}

