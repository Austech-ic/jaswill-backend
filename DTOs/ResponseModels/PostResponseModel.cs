using CMS_appBackend.DTOs;

namespace CMS_appBackend.DTOs.ResponseModels
{
    public class PostsResponseModel : BaseResponse
    {
        public List<PostDto> Data { get; set; } = new List<PostDto>();
    }
    public class PostResponseModel : BaseResponse
    {
        public PostDto  Data {get;set;}
    }
}