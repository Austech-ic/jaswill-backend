using System.Threading.Tasks;
using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.DTOs.ResponseModels;

namespace CMS_appBackend.Interface.Services
{
    public interface ICustomerService
    {
        Task<BaseResponse> CreateCustomer(CreateCustomerRequestModel model);
        Task<CustomersResponseModel> GetAll();
        Task<CustomerResponseModel> GetCustomer(int id);
        Task<CustomerResponseModel> GetCustomerByTypeOfPartner(string typeOfPartner);
        Task<BaseResponse> ForgetPassword(ForgetPasswordRequestModel model, String email);
        Task<BaseResponse> ResetPassword(ResetPasswordRequestModel model, String code);
    }
}