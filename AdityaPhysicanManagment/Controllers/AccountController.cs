using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AdityaPhysicanManagment.Models;
using AdityaPhysicanManagment.Models.Data;
using AdityaPhysicanManagment.ViewModels;

namespace AdityaPhysicanManagment.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home"); // Redirect to home or dashboard
                    }
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
                else
                {
                    ModelState.AddModelError("", "User not found.");
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create Identity user
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Ensure the role exists
                    if (!await _roleManager.RoleExistsAsync(model.Role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(model.Role));
                    }
                    // Add user to the role
                    await _userManager.AddToRoleAsync(user, model.Role);

                    // Save additional details based on role
                    if (model.Role == "Patient")
                    {
                        var patient = new Patient
                        {
                            PatientName = model.PatientName,
                            DateOfBirth = model.DateOfBirth ?? System.DateTime.Now,
                            Disease = model.Disease,
                            PhoneNumber = model.PhoneNumber,
                            Address = model.Address,
                            City = model.City,
                            Country = model.Country,
                            ZipCode = model.ZipCode,
                            Email = model.Email
                        };
                        _context.Patients.Add(patient);
                    }
                    else if (model.Role == "Physician")
                    {
                        var physician = new Physician
                        {
                            DoctorName = model.DoctorName,
                            Specialty = model.Specialty,
                            ContactNumber = model.ContactNumber,
                            Email = model.Email
                        };
                        _context.Physicians.Add(physician);
                    }

                    await _context.SaveChangesAsync();
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }
}
