using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AdityaPhysicanManagment.Models;
using AdityaPhysicanManagment.Services.Interface;
using System.Collections.Generic;
using System.Security.Claims;
using System;

namespace AdityaPhysicanManagment.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // ✅ NEW: Patient Dashboard (Main View)
        public IActionResult Dashboard()
        {
            return View();
        }

        // ✅ MODIFIED: Changed to return PartialView
        public IActionResult Register()
        {
            return PartialView();
        }

        // ✅ MODIFIED: Now redirects to Dashboard after successful registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Patient patient)
        {
            if (!ModelState.IsValid)
                return PartialView(patient);

            var registeredPatient = await _patientService.RegistrPatientAsync(patient);

            if (registeredPatient == null)
            {
                ModelState.AddModelError("", "Failed to register patient.");
                return PartialView(patient);
            }

            return RedirectToAction("Dashboard"); // 🔄 Redirect to the Dashboard
        }

        // ✅ MODIFIED: Changed return type to PartialView
        public async  Task<IActionResult> BookAppointment()
        {
            var doctors = await _patientService.GetAllDoctorsAsync();
            return View(doctors);
        }
        // Previously working code 
        /* // ✅ MODIFIED: Now redirects to Dashboard after successful booking
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> BookAppointment(Appointment appointment)
         {
             if (!ModelState.IsValid)
                 return PartialView(appointment);

             appointment.Status = 1; // Default status: Pending

             bool success = await _patientService.BookAppointmentAsync(appointment);

             if (!success)
             {
                 ModelState.AddModelError("", "Failed to book appointment. The physician might be unavailable.");
                 return PartialView(appointment);
             }

             return RedirectToAction("Dashboard"); // 🔄 Redirect to the Dashboard
         }*/
        /*
                [HttpPost]
               // [ValidateAntiForgeryToken]
                /*public async Task<IActionResult> BookAppointment(Appointment appointment)
                {
                    // Ensure the user is authenticated
                    if (!User.Identity.IsAuthenticated)
                    {
                        ModelState.AddModelError("", "User is not logged in.");
                        return PartialView(appointment);
                    }

                    // 🔹 Fetch the logged-in User ID
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    // Debugging: Print to Console (Check Output Window in VS)
                    Console.WriteLine($"Logged-in User ID: {userId}");

                    // 🔹 Ensure userId is valid before proceeding
                    if (string.IsNullOrEmpty(userId))
                    {
                        ModelState.AddModelError("", "Invalid User ID.");
                        return PartialView(appointment);
                    }

                    // 🔹 Fetch patient details
                    var patient = await _patientService.GetPatientByUserIdAsync(userId);
                    if (patient == null)
                    {
                        ModelState.AddModelError("", "Patient not found. Please register first.");
                        return PartialView(appointment);
                    }

                    // 🔹 Fetch doctor list
                    var doctors = await _patientService.GetAllDoctorsAsync();
                    if (doctors == null || doctors.Count == 0)
                    {
                        ModelState.AddModelError("", "No doctors available.");
                        return PartialView(appointment);
                    }
                    ViewBag.Doctors = doctors;
                    // Pass data to ViewBag
                    ViewBag.PatientName = patient.PatientName;
                    ViewBag.PatientId = patient.UserId; // Ensure correct type
                    ViewBag.Doctors = doctors;

                    // If ModelState is invalid, return view with preloaded data
                    if (!ModelState.IsValid)
                    {
                        return PartialView(appointment);
                    }

                    // 🔹 Assign PatientId to appointment
                    appointment.PatientId = patient.UserId;
                    appointment.Status = 1; // Default status: Pending

                    bool success = await _patientService.BookAppointmentAsync(appointment);

                    if (!success)
                    {
                        ModelState.AddModelError("", "Failed to book appointment. The physician might be unavailable.");
                        return PartialView(appointment);
                    }

                    Console.WriteLine($"Patient Name: {ViewBag.PatientName}");
                    Console.WriteLine($"Total Doctors: {ViewBag.Doctors?.Count ?? 0}");
                    ViewBag.PatientName = patient.PatientName;
                    ViewBag.PatientId = patient.UserId;

                    return RedirectToAction("Dashboard");
                }*/
        [HttpPost]
     //   [ValidateAntiForgeryToken] // Ensure this is present
        public async Task<IActionResult> BookAppointment(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data.");
                return PartialView(appointment);
            }

            // 🔹 Fetch patient details
            var patient = await _patientService.GetPatientByUserIdAsync("2");
            if (patient == null)
            {
                ModelState.AddModelError("", "Patient not found. Please register first.");
                return PartialView(appointment);
            }

            // 🔹 Fetch doctor list
            var doctors = await _patientService.GetAllDoctorsAsync();
            if (doctors == null || doctors.Count == 0)
            {
                ModelState.AddModelError("", "No doctors available.");
                return PartialView(appointment);
            }
            ViewBag.Doctors = doctors;

            // Pass data to ViewBag
            ViewBag.PatientName = patient.PatientName;
            ViewBag.PatientId = patient.UserId;

            // Assign PatientId to appointment
            appointment.PatientId = patient.UserId;
            appointment.Status = 1;

            bool success = await _patientService.BookAppointmentAsync(appointment);
            if (!success)
            {
                ModelState.AddModelError("", "Failed to book appointment. The physician might be unavailable.");
                return PartialView(appointment);
            }

            Console.WriteLine($"Patient Name: {ViewBag.PatientName}");
            Console.WriteLine($"Total Doctors: {ViewBag.Doctors?.Count ?? 0}");

            return RedirectToAction("Dashboard");
        }



        public async Task<IActionResult> GetAppointments(int patientId)
        {
            var appointments = await _patientService.GetAppointmentsByPatientIdAsync(patientId);

            if (appointments == null)  // ✅ Ensure appointments is never null
                appointments = new List<Appointment>();

            return PartialView(appointments);
        }


        // ✅ NEW: Added GetMedicalRecord action (Partial View)
        public async Task<IActionResult> GetMedicalRecord(int appointmentId)
        {
            var medicalRecord = await _patientService.GetMedicalRecordByAppointmentIdAsync(appointmentId);

            if (medicalRecord == null)
                return NotFound();

            return PartialView(medicalRecord);
        }
    }
}
