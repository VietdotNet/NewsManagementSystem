using NewsManagementSystem_Assigment01.Models;
using NewsManagementSystem_Assigment01.Repositories;

namespace NewsManagementSystem_Assigment01.Services
{
    public class TagService
    {
        private readonly ITagRepository _repo;
        public TagService(ITagRepository repo)
        {
            _repo = repo;
        }

        public List<Tag> GetListTag()
        {
            return _repo.GetListTag();
        }
    }
}
