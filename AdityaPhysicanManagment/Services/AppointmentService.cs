using AdityaPhysicanManagment.Models;
using AdityaPhysicanManagment.Services.Interface;
using AppointmentBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdityaPhysicanManagment.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
        }

        public async Task<bool> BookAppointmentAsync(Appointment appointment)
        {
            if (!await _appointmentRepository.IsPhysicianAvailableAsync(appointment.PhysicianId, appointment.StartTime) ||
                !await _appointmentRepository.IsPatientAvailableAsync(appointment.PatientId, appointment.StartTime))
            {
                return false;
            }

            return await _appointmentRepository.BookAppointmentAsync(appointment);
        }

        public async Task<List<DateTime>> GetSuggestedTimeSlotsAsync(int physicianId, DateTime startTime)
        {
            return await _appointmentRepository.GetSuggestedTimeSlotsAsync(physicianId, startTime);
        }

        public async Task<bool> ConfirmOrRejectAppointmentAsync(int appointmentId, int status, int physicianId)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
            if (appointment == null || appointment.PhysicianId != physicianId)
            {
                return false;
            }

            return await _appointmentRepository.UpdateAppointmentStatusAsync(appointmentId, status);
        }
    }
}
