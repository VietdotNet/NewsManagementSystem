using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsManagementSystem_Assigment01.Models;
using NewsManagementSystem_Assigment01.ViewModel;
using System.Diagnostics;

namespace NewsManagementSystem_Assigment01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FunewsManagementContext _context;

        public HomeController(ILogger<HomeController> logger, FunewsManagementContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var newsItems = _context.NewsArticles
                .Select(n => new NewsItemViewModel
                {
                    NewsTitle = n.NewsTitle,
                    ModifiedDate = n.ModifiedDate
                })
                .ToList();

            var viewModel = new NewsListViewModel
            {
                NewsItems = newsItems
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
