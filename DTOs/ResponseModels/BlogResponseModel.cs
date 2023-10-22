using CMS_appBackend.DTOs;

namespace CMS_appBackend.DTOs.ResponseModels
{
    public class BlogsResponseModel : BaseResponse
    {
        public List<BlogDto> Data { get; set; } = new List<BlogDto>();
    }
    public class BlogResponseModel : BaseResponse
    {
        public BlogDto Data { get; set; } 
    }
}