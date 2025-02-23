using NewsManagementSystem_Assigment01.Models;

namespace NewsManagementSystem_Assigment01.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
    }
}
