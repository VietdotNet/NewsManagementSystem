using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NewsManagementSystem_Assigment01.Models;
using System.Security.Claims;
using NewsManagementSystem_Assigment01.ViewModel;
using Microsoft.Extensions.Logging;

namespace NewsManagementSystem_Assigment01.Controllers.Authentication
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly SignInManager<SystemAccount> _signInManager;
        private readonly UserManager<SystemAccount> _userManager;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IConfiguration config, SignInManager<SystemAccount> signInManager, UserManager<SystemAccount> userManager, ILogger<LoginController> logger)
        {
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View("SignInAsync");
        }
        [HttpPost]
        //Khi user đăng nhập sẽ gọi hàm này
        public async Task<IActionResult> SignInAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("ModelState is valid");
                return View(model);
            }

            // 1) Check if this is the Admin from appsettings.json
            var adminEmail = _config["AdminAccount:Email"];
            var adminPassword = _config["AdminAccount:Password"];

            if (model.Email == adminEmail && model.Password == adminPassword)
            {
                // Build claims for Admin
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Email),
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

                // Create identity and sign in
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }
            // 3) If neither Admin nor valid staff/lecturer, return error
            ModelState.AddModelError("", "Invalid Email or Password");
            return View(model);
        }
    }
}
