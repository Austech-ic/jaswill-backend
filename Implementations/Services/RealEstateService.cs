using CMS_appBackend.Identity;
using CMS_appBackend.DTOs.ResponseModels;
using CMS_appBackend.Interface.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using CMS_appBackend.DTOs;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Entities;
using CMS_appBackend.DTOs.RequestModels;

namespace CMS_appBackend.Implementations.Services
{
    public class RealEstateService : IRealEstateService
    {
        private readonly IRealEstateRepository _realEstateRepository;
        private readonly IImageService _imageService;

        private readonly CloudinaryService _cloudinaryService;

        public RealEstateService(IRealEstateRepository realEstateRepository, IImageService imageService, CloudinaryService cloudinaryService)
        {
            _realEstateRepository = realEstateRepository;
            _imageService = imageService;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<BaseResponse> CreateRealEstateAsync(CreateRealEstateRequestModel model)
        {
            var realEstate = await _realEstateRepository.GetAsync(
                x => x.Title == model.Title && x.IsDeleted == false
            );
            if (realEstate != null)
            {
                return new BaseResponse() { Message = "RealEstate Already Exist", Success = false, };
            }
            string cloudinaryUrl = null;
            if (model.ImageUrl != null)
            {
                cloudinaryUrl = await _cloudinaryService.UploadImageToCloudinaryAsync(model.ImageUrl);
            }
            var re = new RealEstate
            {
                Title = model.Title,
                ImageUrl = cloudinaryUrl,
                Description = model.Description,
                Type = model.Type,
                City = model.City,
                Propertylocation = model.Propertylocation,
                NumberOfBedrooms = model.NumberOfBedrooms,
                NumberOfBathrooms = model.NumberOfBathrooms,
                NumberOfFloors = model.NumberOfFloors,
                Content = model.Content,
            };
            var result = await _realEstateRepository.CreateAsync(re);
            return new BaseResponse { Message = "RealEstate Created Successfully", Success = true, };
        }

        public async Task<BaseResponse> DeleteRealEstateAsync(int Id)
        {
            var realEstate = await _realEstateRepository.GetAsync(x => x.Id == Id && x.IsDeleted == false);
            if (realEstate == null)
            {
                return new BaseResponse() { Message = "RealEstate Not Found", Success = false, };
            }
            realEstate.IsDeleted = true;
            await _realEstateRepository.UpdateAsync(realEstate);
            return new BaseResponse() { Message = "RealEstate Deleted Successfully", Success = true, };
        }

        public async Task<RealEstatesResponseModel> GetAllRealEstatesAsync()
        {
            var realEstate = await _realEstateRepository.GetAllAsync();
            if (realEstate == null)
            {
                return new RealEstatesResponseModel
                {
                    Message = "No RealEstate Found",
                    Success = false,
                };
            }
            return new RealEstatesResponseModel
            {
                Data = realEstate.Select(
                        re =>
                            new RealEstateDto
                            {
                                Id = re.Id,
                                Title = re.Title,
                                ImageUrl = re.ImageUrl,
                                Description = re.Description,
                                Type = re.Type,
                                City = re.City,
                                Propertylocation = re.Propertylocation,
                                NumberOfBedrooms = re.NumberOfBedrooms,
                                NumberOfBathrooms = re.NumberOfBathrooms,
                                NumberOfFloors = re.NumberOfFloors,
                                Content = re.Content,
                            }
                    )
                    .ToList(),
                Message = "RealEstate Found",
                Success = true,
            };
        }

        public async Task<RealEstateResponseModel> GetRealEstateByIdAsync(int Id)
        {
            var realEstate = await _realEstateRepository.GetRealEstateById(Id);
            if (realEstate == null)
            {
                return new RealEstateResponseModel
                {
                    Message = "No RealEstate Found",
                    Success = false,
                };
            }
            return new RealEstateResponseModel
            {
                Data = new RealEstateDto
                {
                    Id = realEstate.Id,
                    Title = realEstate.Title,
                    ImageUrl = realEstate.ImageUrl,
                    Description = realEstate.Description,
                    Type = realEstate.Type,
                    City = realEstate.City,
                    Propertylocation = realEstate.Propertylocation,
                    NumberOfBedrooms = realEstate.NumberOfBedrooms,
                    NumberOfBathrooms = realEstate.NumberOfBathrooms,
                    NumberOfFloors = realEstate.NumberOfFloors,
                    Content = realEstate.Content,
                },
                Message = "RealEstate Found",
                Success = true,
            };
        }

        public async Task<BaseResponse> UpdateRealEstateAsync(UpdateRealEstateRequstModel model)
        {
            var realEstate = await _realEstateRepository.GetAsync(x => x.Id == model.Id && x.IsDeleted == false);
            if (realEstate == null)
            {
                return new BaseResponse() { Message = "RealEstate Not Found", Success = false, };
            }
             if (model.ImageUrl != null)
            {
                var cloudinaryImageUrl = await _cloudinaryService.UploadImageToCloudinaryAsync(
                    model.ImageUrl
                );
                realEstate.ImageUrl = cloudinaryImageUrl;
            }
            realEstate.Title = model.Title;
            realEstate.Description = model.Description;
            realEstate.Type = model.Type;
            realEstate.City = model.City;
            realEstate.Propertylocation = model.Propertylocation;
            realEstate.NumberOfBedrooms = model.NumberOfBedrooms;
            realEstate.NumberOfBathrooms = model.NumberOfBathrooms;
            realEstate.NumberOfFloors = model.NumberOfFloors;
            realEstate.Content = model.Content;
            await _realEstateRepository.UpdateAsync(realEstate);
            return new BaseResponse() { Message = "RealEstate Updated Successfully", Success = true, };
        }
    }
}