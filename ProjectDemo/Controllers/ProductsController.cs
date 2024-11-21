using Application.DTOs;
using Domin.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectDemo.Models;

namespace WebUi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IBaseRepository<Product> _BR;
        private readonly IProductRepository _PR;
        public ProductsController(IBaseRepository<Product> BR, IProductRepository PR)
        {
            _BR = BR;
            _PR = PR;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var cat = await _PR.GetAllWithCategoryAsync();
            return Ok(cat);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cat = await _PR.GetByIdWithCategoryAsync(id);

            if (cat != null)
            {
                return Ok(cat);
            }
            return BadRequest("Cannot Find ID");
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Productdto dto)
        {
            var Prod = new Product()
            {
                ProductName = dto.ProductName,
                CategoryId = dto.CategoryId,
                Price = dto.Price,
                stock = dto.stock
            };

            await _BR.AddAsync(Prod);
            return Ok(Prod);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(int id, Productdto dto)
        {
            var Prod = await _PR.GetProductAsync(id);

            if (Prod == null)
            {
                return NotFound("Product not found");
            }
            Prod.ProductName = dto.ProductName;
            Prod.CategoryId = dto.CategoryId;
            Prod.Price = dto.Price;
            Prod.stock = dto.stock;


            await _BR.SaveChangesAsync();
            return Ok(Prod);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var Prod = await _PR.GetProductAsync(id);

            if (Prod == null)
            {
                return NotFound("Category not found");
            }

            await _BR.RemoveAsync(Prod);
            return Ok(Prod);
        }
    }
}
