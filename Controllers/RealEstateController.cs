using CMS_appBackend.DTOs.RequestModels;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Implementations.Services;
using CMS_appBackend.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace CMS_appBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateController : Controller
    {
        private readonly IRealEstateService _realEstateService;
        private readonly CloudinaryService _cloudinaryService;

        public RealEstateController(IRealEstateService realEstateService, CloudinaryService cloudinaryService)
        {
            _realEstateService = realEstateService;
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost("CreateRealEstate")]
        public async Task<IActionResult> CreateRealEstate([FromForm] CreateRealEstateRequestModel model)
        {
            if (model.ImageUrl != null && model.ImageUrl.Length > 0)
            {
                var cloudinaryImageUrl = await _cloudinaryService.UploadImageToCloudinaryAsync(
                    model.ImageUrl
                );
            }
            var realEstate = await _realEstateService.CreateRealEstateAsync(model);

            if (realEstate.Success)
            {
                return Content(realEstate.Message);
            }

            return Content(realEstate.Message);
        }

        [HttpPut("UpdateRealEstate")]
        public async Task<IActionResult> UpdateRealEstate( [FromForm]
            UpdateRealEstateRequstModel model 
        )
        {
           if (model.ImageUrl != null && model.ImageUrl.Length > 0)
            {
                var cloudinaryImageUrl = await _cloudinaryService.UploadImageToCloudinaryAsync(
                    model.ImageUrl
                );
            }
            var realEstate = await _realEstateService.UpdateRealEstateAsync(model);
            if (realEstate.Success == true)
            {
                return Content(realEstate.Message);
            }
            return Content(realEstate.Message);
        }

        [HttpDelete("DeleteRealEstate")]
        public async Task<IActionResult> DeleteRealEstate(int Id)
        {
            var realEstate = await _realEstateService.DeleteRealEstateAsync(Id);
            if (realEstate.Success == true)
            {
                return Content(realEstate.Message);
            }
            return Content(realEstate.Message);
        }

        [HttpGet("GetAllRealEstatesAsync")]
        public async Task<IActionResult> GetAllRealEstatesAsync()
        {
            var realEstates = await _realEstateService.GetAllRealEstatesAsync();
            if (realEstates.Success == true)
            {
                return Ok(realEstates);
            }
            return BadRequest(realEstates);
        }

        [HttpGet("GetRealEstateById/{Id}")]
        public async Task<IActionResult> GetRealEstateByUserId(int Id)
        {
            var realEstates = await _realEstateService.GetRealEstateByIdAsync(Id);
            if (realEstates.Success == true)
            {
                return Ok(realEstates);
            }
            return BadRequest(realEstates);
        }
    }
}