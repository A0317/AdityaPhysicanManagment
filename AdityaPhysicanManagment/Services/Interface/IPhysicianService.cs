using AdityaPhysicanManagment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdityaPhysicanManagment.Services.Interface
{
    public interface IPhysicianService
    {
        Task<List<Appointment>> GetAppointmentsByPhysicianIdAsync(int physicianId);
        Task<bool> UpdateAppointmentStatusAsync(int appointmentId, int status);
        Task<bool> AddOrUpdateMedicalRecordAsync(MedicalRecord medicalRecord);
    }
}
