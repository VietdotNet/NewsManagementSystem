using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Create()
        {
            var model = new NewsArticleViewModel
            {

                CreatedDate = DateTime.Now, // Gán giá trị ngày giờ hiện tại
                CreatedById = User.FindFirstValue(ClaimTypes.NameIdentifier)//Lấy Id của user  
                
            };
            return View(model);
        }
    }
}
