using NewsManagementSystem_Assigment01.Models;
using NewsManagementSystem_Assigment01.ViewModel;

namespace NewsManagementSystem_Assigment01.Repositories
{
    public interface INewsRepository
    {
        void Create(NewsArticle model);
        void Update(NewsArticle model);
        void Delete(NewsArticle model);
        NewsArticle? FindById(string id);

        List<NewsArticle> GetNewsByAccountID(short accountId);

        List<NewsArticle> StatisticNews(DateTime startDate, DateTime endDate);
    }
}
