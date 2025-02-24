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
                    NewsArticleId = n.NewsArticleId,
                    NewsTitle = n.NewsTitle,
                    ModifiedDate = n.ModifiedDate,
                    NewsStatus = n.NewsStatus
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

        [HttpPost]
        public IActionResult ToggleNewsStatus(string id)
        {
            var newsArticle = _context.NewsArticles.FirstOrDefault(n => n.NewsArticleId == id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            // Đảo ngược trạng thái NewsStatus
            newsArticle.NewsStatus = !newsArticle.NewsStatus;
            _context.SaveChanges();

            // Lấy lại danh sách tin tức sau khi thay đổi
            var newsList = _context.NewsArticles
                .Select(n => new NewsItemViewModel
                {
                    NewsArticleId = n.NewsArticleId,
                    NewsTitle = n.NewsTitle,
                    ModifiedDate = n.ModifiedDate,
                    NewsStatus = n.NewsStatus
                })
                .ToList();

            return View("Index", new NewsListViewModel { NewsItems = newsList });
        }

    }
}
