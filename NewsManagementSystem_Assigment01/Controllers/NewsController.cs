using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsManagementSystem_Assigment01.Models;
using NewsManagementSystem_Assigment01.Services;
using NewsManagementSystem_Assigment01.ViewModel;
using System.Security.Claims;

namespace NewsManagementSystem_Assigment01.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsService _service;
        private readonly CategoryService _categoryService;
        private readonly ILogger<NewsController> _logger;

        public NewsController(NewsService service, CategoryService categoryService, ILogger<NewsController> logger)
        {
            _service = service;
            _categoryService = categoryService;
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new NewsArticleViewModel
            {
                CreatedDate = DateTime.Now, // Gán giá trị ngày giờ hiện tại
                CreatedById = User.FindFirstValue(ClaimTypes.NameIdentifier),//Lấy Id của user  
                UpdatedById = User.FindFirstValue(ClaimTypes.NameIdentifier),
                ModifiedDate = DateTime.Now,

            };
            var categories = _categoryService.GetCategories();
            // Tạo SelectList và gán vào ViewBag
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewsArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newsArticle = new NewsArticle
                {
                    NewsArticleId = model.NewsArticleId,
                    NewsTitle = model.NewsTitle,
                    Headline = model.Headline,
                    NewsContent = model.NewsContent,
                    NewsSource = model.NewsSource,
                    CategoryId = model.CategoryId,
                    NewsStatus = model.NewsStatus,
                    CreatedDate = DateTime.Now,
                    CreatedById = short.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out short createdById) ? createdById : (short?)null,
                    UpdatedById = short.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out short updatedById) ? updatedById : (short?)null,
                    ModifiedDate = DateTime.Now
                };

                _service.Create(newsArticle);
                return RedirectToAction("Index", "Home"); // Điều hướng về danh sách bài viết
            }

            // Nếu có lỗi, load lại danh mục
            ViewBag.CategoryId = new SelectList(_categoryService.GetCategories(), "CategoryId", "CategoryName");
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var newsArticle = _service.FindById(id);
            if (newsArticle == null)
            {
                return NotFound();
            }
            return View(newsArticle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var newsArticle = _service.FindById(id);
            if (newsArticle != null)
            {
                _service.Delete(newsArticle);
            }
            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var newsArticle = _service.FindById(id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            var categories = _categoryService.GetCategories();
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", newsArticle.CategoryId);

            var viewModel = new NewsArticleViewModel
            {
                NewsArticleId = newsArticle.NewsArticleId,
                NewsTitle = newsArticle.NewsTitle,
                Headline = newsArticle.Headline,
                NewsContent = newsArticle.NewsContent,
                NewsStatus = newsArticle.NewsStatus,
                CreatedDate = newsArticle.CreatedDate,
                NewsSource = newsArticle.NewsSource,
                CreatedById = newsArticle.CreatedById.ToString(),
                UpdatedById = newsArticle.UpdatedById.ToString(),
                ModifiedDate = newsArticle.ModifiedDate
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NewsArticleViewModel model)
        {
            if(ModelState.IsValid)
            {
                var newsArticle = _service.FindById(model.NewsArticleId);
                if (newsArticle == null)
                {
                    return NotFound();
                }
                else
                {
                    newsArticle.NewsTitle = model.NewsTitle;
                    newsArticle.Headline = model.Headline;
                    newsArticle.NewsContent = model.NewsContent;
                    newsArticle.NewsSource = model.NewsSource;
                    newsArticle.CategoryId = model.CategoryId;
                    newsArticle.NewsStatus = model.NewsStatus;
                    newsArticle.UpdatedById = short.TryParse(model.UpdatedById ?? User.FindFirstValue(ClaimTypes.NameIdentifier), out short result)
                         ? result
                        : (short?)null;
                    newsArticle.ModifiedDate = model.ModifiedDate;

                    _service.Update(newsArticle);
                    TempData["SuccessMessage"] = "Cập nhật bài viết thành công!";

                    return RedirectToAction("Edit", new { id = model.NewsArticleId });
                }

            }
            // Nếu có lỗi, load lại danh mục
            var categories = _categoryService.GetCategories();
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", model.CategoryId);
            return View(model);
        }

        public IActionResult Details(string id)
        {
            var newsArticle = _service.FindById(id);
            return View(newsArticle);
        }



    }
}
