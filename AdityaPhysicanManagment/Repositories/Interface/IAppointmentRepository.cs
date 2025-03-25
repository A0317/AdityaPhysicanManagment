using AdityaPhysicanManagment.Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentBooking.Repository.Interface
{
    public interface IAppointmentRepository
    {
        Task<Appointment?> GetAppointmentByIdAsync(int appointmentId);
        Task<bool> IsPhysicianAvailableAsync(int physicianId, DateTime startTime);
        Task<bool> IsPatientAvailableAsync(int patientId, DateTime startTime);
        Task<bool> BookAppointmentAsync(Appointment appointment);
        Task<List<DateTime>> GetSuggestedTimeSlotsAsync(int physicianId, DateTime startTime);
        Task<bool> UpdateAppointmentStatusAsync(int appointmentId, int status);
    }
}
