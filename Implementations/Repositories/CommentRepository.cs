using Microsoft.EntityFrameworkCore;
using CMS_appBackend.Context;
using CMS_appBackend.Entities;
using CMS_appBackend.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using CMS_appBackend.Context;

namespace CMS_appBackend.Implementations.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationContext context)
        {
            _Context = context;
        }

        public async Task<IList<Comment>> GetAll()
        {
            var comments = await _Context.Comments.Where(x => x.IsDeleted == false).ToListAsync();
            
            return comments;
        }

        public async Task<Comment> GetComment(int id)
        {
            var comment = await _Context.Comments.SingleOrDefaultAsync(x => x.Id == id);
            return comment;
        }

        public async Task<IList<Comment>> GetCommentsByContent(string content)
        {
            var comment = await _Context.Comments.Where(x => x.IsDeleted == false && x.CommentInput.ToLower().Contains(content.ToLower())).ToListAsync();
            return comment;
        }
    }
}
