using System.ComponentModel.DataAnnotations;

namespace CMS_appBackend.DTOs.RequestModel
{
    public class CreateCommentRequestModel
    {
        public string CommentInput { get; set; }
       
    }
    public class UpdateCommentRequestModel
    {
        public string CommentInput { get; set; }
    }
}
