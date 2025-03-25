using System;

namespace AdityaPhysicanManagment.ViewModels
{
    public class RegisterViewModel
    {
        // Identity fields
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        // Role selection (e.g., "Patient" or "Physician")
        public string Role { get; set; }

        // Patient-specific fields (optional if role is Physician)
        public string PatientName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Disease { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        // Physician-specific fields (optional if role is Patient)
        public string DoctorName { get; set; }
        public string Specialty { get; set; }
        public string ContactNumber { get; set; }
    }
}
