using AdityaPhysicanManagment.Models;
using AdityaPhysicanManagment.Models.Data;

using AppointmentBooking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdityaPhysicanManagment.Repository
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicalRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MedicalRecord?> GetMedicalRecordByIdAsync(int medicalRecordId)
        {  
            return await _context.MedicalRecords.FindAsync(medicalRecordId);
        }

        public async Task<List<MedicalRecord>> GetMedicalRecordsByPatientIdAsync(int patientId)
        {
            return await _context.MedicalRecords
                .Where(m => m.Appointment.PatientId == patientId)
                .Include(m => m.Appointment)
                .ToListAsync();
        }

        public async Task<bool> AddMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            await _context.MedicalRecords.AddAsync(medicalRecord);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            var existingRecord = await _context.MedicalRecords.FindAsync(medicalRecord.MedicalRecordId);
            if (existingRecord == null) return false;

            existingRecord.SymptomsReported = medicalRecord.SymptomsReported;
            existingRecord.BloodPressure = medicalRecord.BloodPressure;
            existingRecord.BodyTemperature = medicalRecord.BodyTemperature;
            existingRecord.PulseRate = medicalRecord.PulseRate;
            existingRecord.Diagnosis = medicalRecord.Diagnosis;
            existingRecord.Prescription = medicalRecord.Prescription;

            _context.MedicalRecords.Update(existingRecord);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteMedicalRecordAsync(int medicalRecordId)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(medicalRecordId);
            if (medicalRecord == null) return false;

            _context.MedicalRecords.Remove(medicalRecord);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
