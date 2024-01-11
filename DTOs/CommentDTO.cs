using CMS_appBackend.Entities;

namespace CMS_appBackend.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Detail { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
