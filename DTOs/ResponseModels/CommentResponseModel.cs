namespace CMS_appBackend.DTOs.ResponseModels
{
    public class CommentResponseModel : BaseResponse
    {
        public CommentDTO Data { get; set; }
    }

    public class CommentsResponseModel : BaseResponse
    {
        public ICollection<CommentDTO> Data { get; set; } = new HashSet<CommentDTO>();
    }
}