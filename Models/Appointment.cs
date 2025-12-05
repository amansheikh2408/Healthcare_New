using System;
using System.Collections.Generic;

namespace Healthcare.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        // Foreign keys
        public int DoctorId { get; set; }
        public int UserId { get; set; }

        // Navigation properties
        public Doctor Doctor { get; set; } = null!;  // links to Doctor entity
        public User User { get; set; } = null!;      // links to User entity

        public DateTime SlotTime { get; set; }
    }
}
