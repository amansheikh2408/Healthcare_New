namespace Healthcare.DTOs.ResponseDtos
{
    public class AppointmentResponseDto
    {
        public int Id { get; set; }
        public DateTime SlotTime { get; set; }

        public DoctorInfoDto Doctor { get; set; } = null!;
        public UserInfoDto User { get; set; } = null!;
    }
    public class DoctorInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Specialization { get; set; } = null!;
    }

    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
