using Application.DTOs;
using Domin.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectDemo.Models;
namespace ProjectDemo.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {

        private readonly IBaseRepository<Category> _BR;
        private readonly ICategoryRepository _CR;
        public CategorysController(IBaseRepository<Category> baseRepository, ICategoryRepository categoryRepository)
        {
            _BR = baseRepository;
            _CR = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var cat = await _BR.GetAllAsync();
            return Ok(cat);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cat = await _CR.GetCategoryAsync(id);

            if (cat != null)
            {
                return Ok(cat);
            }
            return BadRequest("Cannot Find ID");
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] List<Categorydto> dtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Categories = new List<Category>();

            foreach (var dto in dtos)
            {
                var cat = new Category()
                {
                    CategoryName = dto.CategoryName,
                };

                await _BR.AddAsync(cat);

                Categories.Add(cat);
            }

            return Ok(Categories);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategory(int id, Categorydto dto)
        {
            var cat = await _CR.GetCategoryAsync(id);

            if (cat == null)
            {
                return NotFound("Category not found");
            }

            cat.CategoryName = dto.CategoryName;
            await _BR.SaveChangesAsync();
            return Ok(cat);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var cat = await _CR.GetCategoryAsync(id);

            if (cat == null)
            {
                return NotFound("Category not found");
            }

            await _BR.RemoveAsync(cat);
            return Ok(cat);
        }
    }
}
