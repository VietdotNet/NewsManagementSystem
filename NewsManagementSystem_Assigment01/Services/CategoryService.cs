using NewsManagementSystem_Assigment01.Models;
using NewsManagementSystem_Assigment01.Repositories;

namespace NewsManagementSystem_Assigment01.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public List<Category> GetCategories() 
        {
            return _repo.GetCategories();
        }
    }
}
