using CMS_appBackend.Entities;

namespace CMS_appBackend.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string CommentInput { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
