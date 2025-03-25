using System.Collections.Generic;
using System.Threading.Tasks;
using AdityaPhysicanManagment.Models;


namespace AppointmentBooking.Repositories.Interfaces
{
    public interface IMedicalRecordRepository
    {
        Task<MedicalRecord?> GetMedicalRecordByIdAsync(int medicalRecordId);
        Task<List<MedicalRecord>> GetMedicalRecordsByPatientIdAsync(int patientId);
        Task<bool> AddMedicalRecordAsync(MedicalRecord medicalRecord);
        Task<bool> UpdateMedicalRecordAsync(MedicalRecord medicalRecord);
        Task<bool> DeleteMedicalRecordAsync(int medicalRecordId);
    }
}
