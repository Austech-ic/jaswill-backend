using System;
using CMS_appBackend.DTOs;
using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.Entities;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Interface.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace CMS_appBackend.Implementations.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        public async Task<BaseResponse> RegisterImage(string model)
        {
            if (model == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Value cannot be nuull"
                };
            }
            
            var cate = new Image
            {
                Path = model
            };
            await _imageRepository.CreateAsync(cate);
            return new BaseResponse
            {
                Success = true,
                Message = "New Image succussfully added"
            };
        }
    }
}