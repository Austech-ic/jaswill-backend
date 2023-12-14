using CMS_appBackend.Context;
using CMS_appBackend.Entities;
using CMS_appBackend.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CMS_appBackend.Implementations.Repositories
{
    public class PostRepository : GenericRepository<Post> , IPostRepository 
    {
        public PostRepository(ApplicationContext Context)
        {
            _Context = Context;
        }
        public async Task<IList<Post>> GetPostByDate(DateTime date)
        {
            return await _Context.Posts.Include(x => x.Images)
            .Where(x => x.CreatedOn == date).ToListAsync();
        }
        public async Task<IList<Post>> GetAllPost()
        {
            return await _Context.Posts.Include(x => x.Images)
            .Where(x => x.IsDeleted == false)
            .ToListAsync();
        }

       public async Task<Post> GetPostById(int id)
        {
            return await _Context.Posts.Include(x => x.Images)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
        }
    }
} 