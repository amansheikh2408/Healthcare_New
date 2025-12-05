namespace Healthcare.DTOs
{
    public class AppointmentDto
    {
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }        // Appointment date
        public TimeSpan StartTime { get; set; }   // Appointment start time
        public TimeSpan EndTime { get; set; }     // Appointment end time
    }
}
