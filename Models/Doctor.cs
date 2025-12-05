//namespace Healthcare.Models
//{
//    public class Doctor
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }
//        public string Email { get; set; }
//        public string Specialization { get; set; }
//        public List<DoctorSlot> Slots { get; set; } = new List<DoctorSlot>();
//    }
//}


using System.Collections.Generic;

namespace Healthcare.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Specialization { get; set; } = null!;

        // Slots navigation
        public List<DoctorSlot> Slots { get; set; } = new List<DoctorSlot>();

        // Appointments navigation
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
