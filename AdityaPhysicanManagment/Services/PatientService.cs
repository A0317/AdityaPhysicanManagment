using AdityaPhysicanManagment.Models;
using AdityaPhysicanManagment.Repository;
using AdityaPhysicanManagment.Services.Interface;
using AppointmentBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdityaPhysicanManagment.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

       public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
      
        public async Task<bool> BookAppointmentAsync(Appointment appointment)
        {
            return await _patientRepository.BookAppointmentAsync(appointment);
        }
        public async Task<Patient> GetPatientByUserIdAsync(string userId)
        {
            return await _patientRepository.GetPatientByUserIdAsync(userId);
        }

        public async Task<List<Physician>> GetAllDoctorsAsync()
        {
            return await _patientRepository.GetAllDoctorsAsync();
        }


        /* 
         * 
         * 
         * public async Task<Appointment> GetAppointmentFormDataAsync(string userId)
          {
              var patient = await _patientRepository.GetPatientByUserIdAsync(userId);
              if (patient == null)
                  throw new Exception("Patient not found.");

              var doctors = await _physicianRepository.GetAllDoctorsAsync();

              return new Appointment
              {
                  PatientId = patient.Id,
                  Patient = patient, // Assign patient object to use its Name in View
                  PhysicianId = 0, // Default value
                  StartTime = DateTime.Now,
                  Status = 1, // Default status
                  Physician = null // We will assign Physician in the dropdown
              };
          }

          */
        public async Task<List<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            return await _patientRepository.GetAppointmentsByPatientIdAsync(patientId);
        }

        public async Task<MedicalRecord> GetMedicalRecordByAppointmentIdAsync(int appointmentId)
        {
            return await _patientRepository.GetMedicalRecordByAppointmentIdAsync(appointmentId);
        }

        public async Task<Patient> RegistrPatientAsync(Patient patient)
        {
            return await _patientRepository.RegisterPatientAsync(patient);
        }

        
    }
}
