//using Healthcare.DTOs;
//using Healthcare.Models;

//namespace Healthcare.Interfaces
//{
//    public interface IAppointmentService
//    {
//        Appointment BookAppointment(AppointmentDto dto);
//        List<Appointment> GetAppointmentsByPatient(int UserId);
//        List<Appointment> GetAppointmentsByDoctor(int doctorId);
//    }
//}
using Healthcare.DTOs;
using Healthcare.DTOs.ResponseDtos;
using Healthcare.Models;

namespace Healthcare.Interfaces
{
    public interface IAppointmentService
    {
        Appointment BookAppointment(AppointmentDto dto);
        List<AppointmentResponseDto> GetAppointmentsByPatient(int UserId);
        List<AppointmentResponseDto> GetAppointmentsByDoctor(int doctorId);
    }
}
