using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsManagementSystem_Assigment01.Services;
using NewsManagementSystem_Assigment01.ViewModel;

namespace NewsManagementSystem_Assigment01.Controllers
{
    public class AdminController : Controller
    {
        private readonly AccountService _service;
        private readonly NewsService _newsService;
        public AdminController(AccountService service, NewsService newsService) 
        {
            _service = service;
            _newsService = newsService;
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

        [HttpGet]
        public IActionResult ReportStatistics(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                // Default: Show last 30 days if no date is provided
                startDate = DateTime.Now.AddDays(-30);
                endDate = DateTime.Now;
            }

            var newsList = _newsService.StatisticNews(startDate.Value, endDate.Value);

            ViewBag.StartDate = startDate.Value.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate.Value.ToString("yyyy-MM-dd");

            return View(newsList);
        }
    }
}
