using AdityaPhysicanManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentBooking.Repository.Interface
{
    public interface IPatientRepository
    {
        Task<bool> BookAppointmentAsync(Appointment appointment);
        Task<List<Appointment>> GetAppointmentsByPatientIdAsync(int patientId);
        Task<MedicalRecord> GetMedicalRecordByAppointmentIdAsync(int appointmentId);
        Task<Patient> RegisterPatientAsync(Patient patients);
        Task<Patient> GetPatientByUserIdAsync(string userId);
Task<List<Physician>> GetAllDoctorsAsync();

    }
}
