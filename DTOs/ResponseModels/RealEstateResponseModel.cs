namespace CMS_appBackend.DTOs.ResponseModels
{
     public class RealEstatesResponseModel : BaseResponse
    {
        public List<RealEstateDto> Data { get; set; } = new List<RealEstateDto>();
    }
    public class RealEstateResponseModel : BaseResponse
    {
        public RealEstateDto  Data {get;set;}
    }
}