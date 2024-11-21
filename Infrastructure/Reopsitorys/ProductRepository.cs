using Domin.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProjectDemo.Data;
using ProjectDemo.Models;

namespace Infrastructure.Reopsitorys
{

    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _Db;
        public ProductRepository(AppDbContext Db)
        {
            _Db = Db;
        }
        public async Task<List<Product>> GetAllWithCategoryAsync()
        {
            return await _Db.products
                .Include(p => p.Category)
                .Select(o => new Product
                {
                    ProductId = o.ProductId,
                    ProductName = o.ProductName,
                    CategoryId = o.Category.CategoryId,
                    Category = new Category
                    {
                        CategoryId = o.Category.CategoryId,
                        CategoryName = o.Category.CategoryName
                    },
                    Price = o.Price,
                    stock = o.stock
                })
                .ToListAsync();
        }

        public async Task<Product> GetByIdWithCategoryAsync(int id)
        {
            var product = await _Db.products
                .Include(p => p.Category)
                .Select(o => new Product
                {
                    ProductId = o.ProductId,
                    ProductName = o.ProductName,
                    CategoryId = o.Category.CategoryId,
                    Category = new Category
                    {
                        CategoryId = o.Category.CategoryId,
                        CategoryName = o.Category.CategoryName
                    },
                    Price = o.Price,
                    stock = o.stock
                })
                .FirstOrDefaultAsync(p => p.ProductId == id);

            return product;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _Db.products.FirstOrDefaultAsync(o => o.ProductId == id);
        }
    }
}
