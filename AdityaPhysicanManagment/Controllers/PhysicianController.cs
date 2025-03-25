using AdityaPhysicanManagment.Models;
using AdityaPhysicanManagment.Repository;
using AppointmentBooking.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdityaPhysicanManagment.Controllers
{
    public class PhysicianController : Controller
    {
        private readonly IPhysicianRepository _physicianRepository;

        public PhysicianController(IPhysicianRepository physicianRepository)
        {
            _physicianRepository = physicianRepository;
        }

        // NEW: Physician Dashboard action to load the dashboard view with tabs
        public IActionResult PhysicianDashboard()
        {
            // Optionally, you can load common data here (for example, physician details).
            return View();
        }

        // View Appointments for a Physician
        public async Task<IActionResult> Appointments(int physicianId)
        {
            var appointments = await _physicianRepository.GetAppointmentsByPhysicianIdAsync(physicianId);
            return View(appointments);
        }

        // Update Appointment Status
        [HttpPost]
        public async Task<IActionResult> UpdateAppointmentStatus(int appointmentId, int status)
        {
            var success = await _physicianRepository.UpdateAppointmentStatusAsync(appointmentId, status);
            if (!success)
            {
                TempData["Error"] = "Failed to update appointment status.";
            }
            // Redirect to dashboard or Appointments view as needed.
            return RedirectToAction("PhysicianDashboard");
        }

        // Add or Update Medical Record (GET)
        [HttpGet]
        public IActionResult AddOrUpdateMedicalRecord(int appointmentId)
        {
            var model = new MedicalRecord { AppointmentId = appointmentId };
            return View(model);
        }

        // Add or Update Medical Record (POST)
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateMedicalRecord(MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                bool result = await _physicianRepository.AddOrUpdateMedicalRecordAsync(medicalRecord);
                if (result)
                    return RedirectToAction("PhysicianDashboard");
            }
            TempData["Error"] = "Failed to save medical record.";
            return View(medicalRecord);
        }
    }
}
