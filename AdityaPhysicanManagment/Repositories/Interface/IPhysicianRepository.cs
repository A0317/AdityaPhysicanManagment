using AdityaPhysicanManagment.Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentBooking.Repository.Interface
{
    public interface IPhysicianRepository
    {
        Task<List<Appointment>> GetAppointmentsByPhysicianIdAsync(int physicianId);
        Task<bool> UpdateAppointmentStatusAsync(int appointmentId, int status);
        Task<bool> IsPhysicianAvailableAsync(int physicianId, DateTime startTime);
        Task<bool> AddOrUpdateMedicalRecordAsync(MedicalRecord medicalRecord);
    }
}
