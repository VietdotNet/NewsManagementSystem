using NewsManagementSystem_Assigment01.Models;

namespace NewsManagementSystem_Assigment01.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly FunewsManagementContext _context;

        public TagRepository(FunewsManagementContext context) {  _context = context; }
        public List<Tag> GetListTag()
        {
            return _context.Tags.ToList();
        }
    }
}
