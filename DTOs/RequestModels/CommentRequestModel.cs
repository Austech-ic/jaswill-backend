using System.ComponentModel.DataAnnotations;

namespace CMS_appBackend.DTOs.RequestModel
{
    public class CommentRequestModel
    {
        
    }
    public class CreateCommentRequestModel
    {
        [Required]
        [StringLength(maximumLength: 300, MinimumLength = 5)]
        public string Detail { get; set; }
       
    }
    public class UpdateCommentRequestModel
    {
        public string Detail { get; set; }
    }
}
