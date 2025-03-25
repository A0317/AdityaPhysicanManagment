using AdityaPhysicanManagment.Models;
using AdityaPhysicanManagment.Models.Data;
using AppointmentBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentBooking.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _context.Appointments.FindAsync(appointmentId);
        }

        public async Task<bool> IsPhysicianAvailableAsync(int physicianId, DateTime startTime)
        {
            return !await _context.Appointments.AnyAsync(a => a.PhysicianId == physicianId && a.StartTime == startTime);
        }

        public async Task<bool> IsPatientAvailableAsync(int patientId, DateTime startTime)
        {
            return !await _context.Appointments.AnyAsync(a => a.PatientId == patientId && a.StartTime == startTime);
        }

        public async Task<bool> BookAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<DateTime>> GetSuggestedTimeSlotsAsync(int physicianId, DateTime startTime)
        {
            var occupiedSlots = await _context.Appointments
                .Where(a => a.PhysicianId == physicianId)
                .Select(a => a.StartTime)
                .ToListAsync();

            List<DateTime> suggestedSlots = new List<DateTime>();
            DateTime earliestSlot = startTime.Date.AddHours(9);  // Clinic opens at 9 AM
            DateTime latestSlot = startTime.Date.AddHours(17);  // Closes at 5 PM

            for (DateTime time = earliestSlot; time < latestSlot; time = time.AddMinutes(30))
            {
                if (!occupiedSlots.Contains(time))
                {
                    suggestedSlots.Add(time);
                }
            }

            return suggestedSlots;
        }

        public async Task<bool> UpdateAppointmentStatusAsync(int appointmentId, int status)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment == null) return false;

            appointment.Status = status;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
