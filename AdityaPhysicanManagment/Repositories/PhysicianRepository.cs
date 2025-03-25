using AdityaPhysicanManagment.Models;
using AdityaPhysicanManagment.Models.Data;
using AppointmentBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdityaPhysicanManagment.Repository
{
    public class PhysicianRepository : IPhysicianRepository
    {
        private readonly ApplicationDbContext _context;

        public PhysicianRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetAppointmentsByPhysicianIdAsync(int physicianId)
        {
            return await _context.Appointments
                .Where(a => a.PhysicianId == physicianId)
                .Include(a => a.Patient)
                .ToListAsync();
        }

        public async Task<bool> UpdateAppointmentStatusAsync(int appointmentId, int status)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment == null)
                return false;

            appointment.Status = status;
            _context.Appointments.Update(appointment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsPhysicianAvailableAsync(int physicianId, DateTime startTime)
        {
            return !await _context.Appointments
                .AnyAsync(a => a.PhysicianId == physicianId && a.StartTime == startTime && a.Status == 2);
        }

        public async Task<bool> AddOrUpdateMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            var existingRecord = await _context.MedicalRecords
                .FirstOrDefaultAsync(m => m.AppointmentId == medicalRecord.AppointmentId);

            if (existingRecord != null)
            {
                // Update existing medical record
                existingRecord.SymptomsReported = medicalRecord.SymptomsReported;
                existingRecord.BloodPressure = medicalRecord.BloodPressure;
                existingRecord.BodyTemperature = medicalRecord.BodyTemperature;
                existingRecord.PulseRate = medicalRecord.PulseRate;
                existingRecord.Diagnosis = medicalRecord.Diagnosis;
                existingRecord.Prescription = medicalRecord.Prescription;

                _context.MedicalRecords.Update(existingRecord);
            }
            else
            {
                // Add new medical record
                await _context.MedicalRecords.AddAsync(medicalRecord);
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
