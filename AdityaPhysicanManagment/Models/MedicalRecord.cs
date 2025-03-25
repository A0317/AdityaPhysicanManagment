using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdityaPhysicanManagment.Models
{
    public class MedicalRecord
    {
        [Key]
        public int MedicalRecordId { get; set; } // Primary Key (Auto-generated)

        [Required]
        public int AppointmentId { get; set; } // FK to Appointment

        [Required]
        public string SymptomsReported { get; set; } // Details of symptoms, duration

        [Required]
        public int BloodPressure { get; set; } // Measured BP

        [Required]
        public double BodyTemperature { get; set; } // Measured in Fahrenheit

        [Required]
        public int PulseRate { get; set; } // Heart rate

        [Required]
        public string Diagnosis { get; set; } // Physician's diagnosis

        [Required]
        public string Prescription { get; set; } // Medication and dosage

        [ForeignKey("AppointmentId")]
        public Appointment? Appointment { get; set; }
    }
}
