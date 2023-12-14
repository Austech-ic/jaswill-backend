using CMS_appBackend.Entities;
using CMS_appBackend.DTOs.ResponseModels;
namespace CMS_appBackend.Interface.Services
{
    public interface IImageService
    {
        Task<BaseResponse> RegisterImage(string model);
    }
}