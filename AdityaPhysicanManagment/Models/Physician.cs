using System.ComponentModel.DataAnnotations;
using System;

namespace AdityaPhysicanManagment.Models
{
    public class Physician
    {

        [Key]
        public int UserId { get; set; } // Primary Key

        [Required]
        public string DoctorName { get; set; }

        [Required]
        public string Specialty { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
