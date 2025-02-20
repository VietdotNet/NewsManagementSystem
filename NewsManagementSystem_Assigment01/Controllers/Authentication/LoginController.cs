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
using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;

namespace NewsManagementSystem_Assigment01.Controllers.Authentication
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<LoginController> _logger;
        private readonly FunewsManagementContext _context;

        public LoginController(IConfiguration config, ILogger<LoginController> logger, FunewsManagementContext context)
        {
            _config = config;
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View("SignIn");
        }

        [HttpPost]
        public async Task<IActionResult> SignInAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("ModelState is invalid");
                return View(model);
            }

            var adminConfig = _config.GetSection("AdminAccount");
            string adminEmail = adminConfig["Email"];
            string adminPassword = adminConfig["Password"];

            if (model.Email == adminEmail && model.Password == adminPassword)
            {
                //Tạo ra list Claims(Các thông tin người dùng đang đăng nhập vào hệ thống) -> xác thực người dùng này là ai, có vai trò gì...
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Admin"),
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                //Lưu trữ list Claims trong CookieAuthentication
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //Thực hiện đăng nhập dựa trên Claims
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));

                return RedirectToAction("SecurePage");
            }

            var user = _context.SystemAccounts.Where(x => (x.AccountEmail == model.Email) && (x.AccountPassword == model.Password)).FirstOrDefault();
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.AccountName),
                    new Claim(ClaimTypes.Email, user.AccountEmail),
                    new Claim(ClaimTypes.Role, user.AccountRole.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.AccountId.ToString())
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));

                return RedirectToAction("SecurePage");
            }
            else
            {
                _logger.LogWarning("Đăng nhập thất bại: Sai email hoặc mật khẩu.");
                ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng!");
                return View(model);
            }
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }




    }
}
