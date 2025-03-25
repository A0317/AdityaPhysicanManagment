using AdityaPhysicanManagment.Models;
using AdityaPhysicanManagment.Services.Interface;
using AppointmentBooking.Repository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdityaPhysicanManagment.Services
{
    public class PhysicianService : IPhysicianService
    {
        private readonly IPhysicianRepository _physicianRepository;

        public PhysicianService(IPhysicianRepository physicianRepository)
        {
            _physicianRepository = physicianRepository;
        }

        public async Task<List<Appointment>> GetAppointmentsByPhysicianIdAsync(int physicianId)
        {
            return await _physicianRepository.GetAppointmentsByPhysicianIdAsync(physicianId);
        }

        public async Task<bool> UpdateAppointmentStatusAsync(int appointmentId, int status)
        {
            return await _physicianRepository.UpdateAppointmentStatusAsync(appointmentId, status);
        }

        public async Task<bool> AddOrUpdateMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            return await _physicianRepository.AddOrUpdateMedicalRecordAsync(medicalRecord);
        }
    }
}
