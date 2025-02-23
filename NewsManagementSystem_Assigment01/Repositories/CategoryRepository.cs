using NewsManagementSystem_Assigment01.Models;

namespace NewsManagementSystem_Assigment01.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FunewsManagementContext _context;

        public CategoryRepository(FunewsManagementContext context)
        {
            _context = context;
        }
        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
