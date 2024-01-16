using CMS_appBackend.Entities;

namespace CMS_appBackend.Interface.Repositories
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<Comment> GetComment(int id);
        Task<IList<Comment>> GetAll();
        Task<IList<Comment>> GetCommentsByContent(string content);
    }
}
