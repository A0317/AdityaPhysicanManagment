using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace AdityaPhysicanManagment.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; } // Primary Key

        [Required]
        public int PhysicianId { get; set; } // FK to Physician

        [Required]
        public int PatientId { get; set; } // FK to Patient

        [Required]
        public DateTime StartTime { get; set; } // Appointment Time

        [Required]
        [Range(1, 3)]
        public int Status { get; set; } = 1;  // 1 = Pending, 2 = Confirmed, 3 = Rejected

        [ForeignKey("PhysicianId")]
        public Physician? Physician { get; set; }

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }
    }
}