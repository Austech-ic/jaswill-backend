using CMS_appBackend.Entities;

namespace CMS_appBackend.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Detail { get; set; }
        public string? UserName { get; set; }
        public string BlogerName { get; set; }
        public string? UserImage { get; set; }
        public string? BlogerImage { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
