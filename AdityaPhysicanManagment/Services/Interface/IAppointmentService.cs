using AdityaPhysicanManagment.Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdityaPhysicanManagment.Services.Interface
{
    public interface IAppointmentService
    {
        Task<Appointment?> GetAppointmentByIdAsync(int appointmentId);
        Task<bool> BookAppointmentAsync(Appointment appointment);
        Task<List<DateTime>> GetSuggestedTimeSlotsAsync(int physicianId, DateTime startTime);
        Task<bool> ConfirmOrRejectAppointmentAsync(int appointmentId, int status, int physicianId);
    }
}
