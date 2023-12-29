using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS_appBackend.Entities;
namespace CMS_appBackend.Interface.Repositories
{
    public interface IBlogRepository : IGenericRepository<Blog>
    {
       Task<List<Blog>> GetBlogsByDateAsync(DateTime Date);
       Task<Blog> GetBlogByIdAsync(int id);
       Task<IList<Blog>> GetAllBlogsAsync();
       Task<Blog> GetBlogByTitleAsync(string title);
       Task<List<Blog>> GetBlogsToDisplayAsync();
    }
}