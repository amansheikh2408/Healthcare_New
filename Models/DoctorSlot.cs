namespace Healthcare.Models
{
    public class DoctorSlot
    {
       


        public int Id { get; set; }
        public DateTime Date { get; set; }       // Slot date
        public TimeSpan StartTime { get; set; }  // Slot start time
        public TimeSpan EndTime { get; set; }    // Slot end time
        public bool IsBooked { get; set; }       // Slot booked or not
        public int? UserId { get; set; }         // Assigned patient (optional)

        // Foreign key to Doctor
        public int DoctorId { get; set; }

        // Navigation property
        public Doctor Doctor { get; set; } = null!;
    }
}
