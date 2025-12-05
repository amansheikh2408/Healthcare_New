namespace Healthcare.DTOs
{
    public class DoctorDtoResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public List<DoctorSlotDtoResponse> Slots { get; set; } = new();
    }
    public class DoctorSlotDtoResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsBooked { get; set; }
        public int? UserId { get; set; }
    }
}
