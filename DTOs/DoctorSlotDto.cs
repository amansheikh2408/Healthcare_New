namespace Healthcare.DTOs
{
    public class DoctorSlotDto
    {
        public DateTime Date { get; set; }        // Slot ka date
        public TimeSpan StartTime { get; set; }   // Slot start
        public TimeSpan EndTime { get; set; }     // Slot end
    }
}
