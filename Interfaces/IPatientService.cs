using Healthcare.DTOs;
using Healthcare.Models;
using static Healthcare.DTOs.UserDtos;

namespace Healthcare.Interfaces
{
    public interface IPatientService
    {
        User AddPatient(UserDtos.UserDto dto );
        List<User> GetAllPatients();
       User GetPatientById(int id);
    }
}
