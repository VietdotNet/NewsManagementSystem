using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsManagementSystem_Assigment01.Services;
using NewsManagementSystem_Assigment01.ViewModel;

namespace NewsManagementSystem_Assigment01.Controllers
{
    public class AdminController : Controller
    {
        private readonly AccountService _service;
        public AdminController(AccountService service) 
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManageUser()
        {
            var user = _service.GetListUser();
            var viewModel = new ListUserViewModel
            {
                ListUser = user
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ToggleAccountStatus(short id)
        {
            var account = _service.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }

            _service.AccountStatus(account);

            return RedirectToAction("ManageUser", "Admin"); // Quay về danh sách
        }
    }
}
