using NewsManagementSystem_Assigment01.Models;
using NewsManagementSystem_Assigment01.Repositories;

namespace NewsManagementSystem_Assigment01.Services
{
    public class NewsService
    {
        private readonly INewsRepository _repo;

        public NewsService(INewsRepository repo)
        {
            _repo = repo;
        }

        public void Create(NewsArticle model)
        {
            _repo.Create(model);
        }
        
        public void Update(NewsArticle model)
        {
            _repo.Update(model);
        }

        public void Delete(NewsArticle model)
        {
            _repo.Delete(model);
        }

        public NewsArticle? FindById(string id)
        {
            return _repo.FindById(id);
        }
    }
}
