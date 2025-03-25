using AdityaPhysicanManagment.Models;
using AdityaPhysicanManagment.Services.Interface;
using AppointmentBooking.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentBooking.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public async Task<MedicalRecord?> GetMedicalRecordByIdAsync(int medicalRecordId)
        {
            return await _medicalRecordRepository.GetMedicalRecordByIdAsync(medicalRecordId);
        }

        public async Task<List<MedicalRecord>> GetMedicalRecordsByPatientIdAsync(int patientId)
        {
            return await _medicalRecordRepository.GetMedicalRecordsByPatientIdAsync(patientId);
        }

        public async Task<bool> AddMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            return await _medicalRecordRepository.AddMedicalRecordAsync(medicalRecord);
        }

        public async Task<bool> UpdateMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            return await _medicalRecordRepository.UpdateMedicalRecordAsync(medicalRecord);
        }

        public async Task<bool> DeleteMedicalRecordAsync(int medicalRecordId)
        {
            return await _medicalRecordRepository.DeleteMedicalRecordAsync(medicalRecordId);
        }
    }
}
