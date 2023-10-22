using CMS_appBackend.Context;
using CMS_appBackend.Entities;
using CMS_appBackend.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CMS_appBackend.Implementations.Repositories
{
    public class BlogRepository : GenericRepository<Blog> , IBlogRepository
    {
        public BlogRepository(ApplicationContext Context)
        {
            _Context = Context;
        }

        
        public async Task<List<Blog>> GetBlogsByDateAsync(DateTime date)
        {
            return await _Context.Blogs
            .Include(x => x.Posts)
            .Where(x => x.IsDeleted == false)
            .ToListAsync();
        }

        public async Task<List<Blog>> GetBlogsToDisplayAsync()
        {
            var Blog = await _Context.Blogs.Include(x => x.Posts).Where(x => x.IsDeleted == false).ToListAsync();
            return Blog;
        }
    }
}