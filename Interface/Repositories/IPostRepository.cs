using CMS_appBackend.Entities;
namespace CMS_appBackend.Interface.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<IList<Post>> GetPostByDate(DateTime date);
        Task<IList<Post>> GetAllPost();
        Task<Post> GetPostById(int id);
    }
}
