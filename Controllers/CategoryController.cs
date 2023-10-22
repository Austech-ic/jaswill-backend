using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CMS_appBackend.DTOs.RequestModel;
using CMS_appBackend.Interface.Services;

namespace CMS_appBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromForm] CreateCategoryRequestModel model)
        {
            var add = await _categoryService.AddCategory(model);
            if (add.Success == false)
            {
                return BadRequest(add);
            }
            return Ok(add);
        }

        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory([FromForm] UpdateCategoryRequestModel model, [FromRoute]int id)
        {
            var update = await _categoryService.UpdateCategory(model, id);
            if (update.Success == false)
            {
                return BadRequest(update);
            }
            return Ok(update);
        }

        [HttpGet("GetCategoriesByName/{name}")]
        public async Task<IActionResult> GetCategoriesByName([FromRoute]string name)
        {
            var catg = await _categoryService.GetCategoriesByName(name);
            if (catg.Success == false)
            {
                return BadRequest(catg);
            }
            return Ok(catg);
        }

        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            var all = await _categoryService.GetAll();
            if(all.Success == false)
            {
                return BadRequest(all);
            }
            return Ok(all);
        }

        [HttpGet("GetCategoryById/{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute]int id)
        {
            var catg = await _categoryService.GetById(id);
            if(catg.Success == false)
            {
                return BadRequest(catg);
            }
            return Ok(catg);
        }

        [HttpGet("GetAllCategoryWithInfo")]
        public async Task<IActionResult> GetAllCategoryWithInfo()
        {
            var categories = await _categoryService.GetAllWithInfo();
            if(categories.Success == false)
            {
                return BadRequest(categories);
            }
            return Ok(categories);
        }
    }
}
