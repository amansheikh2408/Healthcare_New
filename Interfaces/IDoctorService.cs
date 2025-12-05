using Healthcare.DTOs;
using Healthcare.Models;

namespace Healthcare.Interfaces
{
    public interface IDoctorService
    {

        Doctor AddDoctor(DoctorDto dto);
        List<Doctor> GetAllDoctors();
        Doctor GetDoctorById(int id);
        void AddDoctorSlot(int doctorId, DoctorSlotDto slotDto);
    }
}
