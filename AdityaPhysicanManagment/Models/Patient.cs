using System;
using System.ComponentModel.DataAnnotations;

namespace AdityaPhysicanManagment.Models
{
    public class Patient
    {

        [Key]
        public int UserId { get; set; } // Primary Key

        [Required]
        public string PatientName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Disease { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Address { get; set; } // Optional

        [Required]
        public string City { get; set; }

        public string Country { get; set; } // Optional

        public string ZipCode { get; set; } // Optional

        [Required]
        public string Email { get; set; }
    }
}
