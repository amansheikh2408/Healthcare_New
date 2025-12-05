using Healthcare.DTOs;
using Healthcare.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;

        public AdminController(IDoctorService doctorService, IPatientService patientService, IAppointmentService appointmentService)
        {
            _doctorService = doctorService;
            _patientService = patientService;
            _appointmentService = appointmentService;
        }

        [HttpPost("add-doctor")]
        public IActionResult AddDoctor([FromBody] DoctorDto dto) => Ok(_doctorService.AddDoctor(dto));

        [HttpPost("add-slot/{doctorId}")]
        public IActionResult AddDoctorSlot(int doctorId, [FromBody] DoctorSlotDto dto)
        {
            _doctorService.AddDoctorSlot(doctorId, dto);
            return Ok("Slot added");
        }

        [HttpGet("doctor-appointments/{doctorId}")]
        public IActionResult GetDoctorAppointments(int doctorId)
            => Ok(_appointmentService.GetAppointmentsByDoctor(doctorId));

        [HttpGet("patient-appointments/{patientId}")]
        public IActionResult GetPatientAppointments(int patientId)
            => Ok(_appointmentService.GetAppointmentsByPatient(patientId));
    }

}

