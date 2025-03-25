using AdityaPhysicanManagment.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdityaPhysicanManagment.Services.Interface
{
    public interface IPatientService
    {
        Task<bool> BookAppointmentAsync(Appointment appointment);
        Task<Patient> RegistrPatientAsync(Patient patient);
        Task<List<Appointment>> GetAppointmentsByPatientIdAsync(int patientId);
        Task<MedicalRecord> GetMedicalRecordByAppointmentIdAsync(int appointmentId);
        //  Task<List<Physician>> GetAllDoctorsAsync();
        Task<Patient> GetPatientByUserIdAsync(string userId);
        Task<List<Physician>> GetAllDoctorsAsync();


    }
}
