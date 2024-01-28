using System.ComponentModel.DataAnnotations;

namespace CMS_appBackend.DTOs.RequestModel
{
    public class CreateCommentRequestModel
    {
        public string UserName { get; set; }
        public string CommentInput { get; set; }
        public int BlogId { get; set; }
       
    }
    public class UpdateCommentRequestModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string CommentInput { get; set; }
    }
}
