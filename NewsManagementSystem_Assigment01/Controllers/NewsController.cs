using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsManagementSystem_Assigment01.Models;
using NewsManagementSystem_Assigment01.ViewModel;
using System.Security.Claims;

namespace NewsManagementSystem_Assigment01.Controllers
{
    public class NewsController : Controller
    {
        private readonly FunewsManagementContext _context;
        public NewsController(FunewsManagementContext context)
        {
            _context = context;
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
            var categories = _context.Categories.ToList();
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

                _context.NewsArticles.Add(newsArticle);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home"); // Điều hướng về danh sách bài viết
            }

            // Nếu có lỗi, load lại danh mục
            ViewBag.CategoryId = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var newsArticle = _context.NewsArticles.Find(id);
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
            var newsArticle = _context.NewsArticles.Find(id);
            if (newsArticle != null)
            {
                _context.NewsArticles.Remove(newsArticle);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var newsArticle = _context.NewsArticles.Find(id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            var categories = _context.Categories.ToList();
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", newsArticle.CategoryId);

            var viewModel = new NewsArticleViewModel
            {
                NewsArticleId = newsArticle.NewsArticleId,
                NewsTitle = newsArticle.NewsTitle,
                NewsContent = newsArticle.NewsContent,
                NewsStatus = newsArticle.NewsStatus,
                //CategoryId = newsArticle.CategoryId,
               /* CreatedDate = newsArticle.CreatedDate*/
                ModifiedDate = newsArticle.ModifiedDate
            };

            return View(viewModel);
        }




    }
}
