namespace CMS_appBackend.DTOs.ResponseModels
{
    public class CustomerResponseModel : BaseResponse
    {
        public CustomerDto Data { get; set; }
    }

    public class CustomersResponseModel : BaseResponse
    {
        public ICollection<CustomerDto> Data { get; set; } = new HashSet<CustomerDto>();
    }
}