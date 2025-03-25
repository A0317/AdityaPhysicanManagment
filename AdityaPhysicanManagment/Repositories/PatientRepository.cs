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

    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<bool> BookAppointmentAsync(Appointment appointment)
        //{
        //    // Check if physician is available
        //    bool isPhysicianAvailable = !await _context.Appointments
        //        .AnyAsync(a => a.PhysicianId == appointment.PhysicianId && a.StartTime == appointment.StartTime);

        //    if (!isPhysicianAvailable)
        //        return false; // Physician is unavailable

        //    // Check if patient already has an appointment at the same time
        //    bool isPatientAvailable = !await _context.Appointments
        //        .AnyAsync(a => a.PatientId == appointment.PatientId && a.StartTime == appointment.StartTime);

        //    if (!isPatientAvailable)
        //        return false; // Patient already booked an appointment at that time
        //    // also check if patien is alredy in data base do not insert patient information in database

        //    await _context.Appointments.AddAsync(appointment);
        //    return await _context.SaveChangesAsync() > 0;
        //}

        //public async Task<bool> BookAppointmentAsync(Appointment appointment)
        //{
        //    // Check if physician is available
        //    bool isPhysicianAvailable = !await _context.Appointments
        //        .AnyAsync(a => a.PhysicianId == appointment.PhysicianId && a.StartTime == appointment.StartTime);

        //    if (!isPhysicianAvailable)
        //        return false; // Physician is unavailable

        //    // Check if patient already has an appointment at the same time
        //    bool isPatientAvailable = !await _context.Appointments
        //        .AnyAsync(a => a.PatientId == appointment.PatientId && a.StartTime == appointment.StartTime);

        //    if (!isPatientAvailable)
        //        return false; // Patient already booked an appointment at that time

        //    // Check if patient already exists in the database
        //   /* var existingPatient = await _context.Patients.FindAsync(appointment.PatientId);
        //    if (existingPatient == null)
        //    {
        //        // Insert patient information if not already in the database
        //        await _context.Patients.AddAsync(appointment.Patient);
        //    }

        //    var existingPh = await _context.Physicians.FindAsync(appointment.PatientId);
        //    if (existingPh == null)
        //    {
        //        // Insert Physician information if not already in the database
        //        await _context.Physicians.AddAsync(appointment.Physician);
        //    }*/

        //    await _context.Appointments.AddAsync(appointment);
        //    return await _context.SaveChangesAsync() > 0;
        //}
        public async Task<List<Physician>> GetAllDoctorsAsync()
        {
            return await _context.Physicians.ToListAsync();
        }
        public async Task<bool> BookAppointmentAsync(Appointment appointment)
        {
            // Fetch the existing Patient and Physician by ID (without updating them)
            //var existingPatient = await _context.Patients.FindAsync(appointment.PatientId);
            //var existingPhysician = await _context.Physicians.FindAsync(appointment.PhysicianId);

          //  Check if physician is available
                bool isPhysicianAvailable = !await _context.Appointments
                    .AnyAsync(a => a.PhysicianId == appointment.PhysicianId && a.StartTime == appointment.StartTime);

            if (!isPhysicianAvailable)
                return false; // Physician is unavailable

            // Check if patient already has an appointment at the same time
            bool isPatientAvailable = !await _context.Appointments
                .AnyAsync(a => a.PatientId == appointment.PatientId && a.StartTime == appointment.StartTime);

            if (!isPatientAvailable)
                return false; // Patient already booked an appointment at that time


            //if (existingPatient == null || existingPhysician == null)
            //{
            //     throw new Exception("Patient or Physician not found");
            //}

            //// Assign existing references (avoid modifying Patient/Physician)
            //appointment.Patient = existingPatient;
            //appointment.Physician = existingPhysician;

            // Make sure EF doesn't try to update the existing patient/physician
            //_context.Entry(existingPatient).State = EntityState.Unchanged;
            //_context.Entry(existingPhysician).State = EntityState.Unchanged;

            // Add only the Appointment
            _context.Appointments.Add(appointment);
           return await _context.SaveChangesAsync() > 0;
        }

     /* public async Task<Patient> GetPatientByUserIdAsync(string userId)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);
        }*/

        public async Task<Patient> GetPatientByUserIdAsync(string userId)
        {
            if (!int.TryParse(userId, out int parsedUserId))
            {
                return null; // Return null if conversion fails
            }

            return await _context.Patients.FirstOrDefaultAsync(p => p.UserId == parsedUserId);
        }




        public async Task<List<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            return await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Physician)
                .ToListAsync();
        }

        public async Task<MedicalRecord> GetMedicalRecordByAppointmentIdAsync(int appointmentId)
        {
            return await _context.MedicalRecords
                .FirstOrDefaultAsync(m => m.AppointmentId == appointmentId);
        }
        public async Task<Patient> RegisterPatientAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        // dto Auteh: true  data: user 


    }
}

