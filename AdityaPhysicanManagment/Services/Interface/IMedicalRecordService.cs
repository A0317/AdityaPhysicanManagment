using AdityaPhysicanManagment.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdityaPhysicanManagment.Services.Interface
{
    public interface IMedicalRecordService
    {
        Task<MedicalRecord?> GetMedicalRecordByIdAsync(int medicalRecordId);
        Task<List<MedicalRecord>> GetMedicalRecordsByPatientIdAsync(int patientId);
        Task<bool> AddMedicalRecordAsync(MedicalRecord medicalRecord);
        Task<bool> UpdateMedicalRecordAsync(MedicalRecord medicalRecord);
        Task<bool> DeleteMedicalRecordAsync(int medicalRecordId);
    }
}
