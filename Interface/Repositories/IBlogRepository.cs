using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS_appBackend.Entities;
namespace CMS_appBackend.Interface.Repositories
{
    public interface IBlogRepository : IGenericRepository<Blog>
    {
       Task<List<Blog>> GetBlogsByDateAsync(DateTime AuctionDate);
       Task<List<Blog>> GetBlogsToDisplayAsync();
    }
}