using Domin.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProjectDemo.Data;
using ProjectDemo.Models;

namespace Infrastructure.Reopsitorys
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _Db;


        public CategoryRepository(AppDbContext Db)
        {
            _Db = Db;
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await _Db.categories.FirstOrDefaultAsync(o => o.CategoryId == id);
        }
    }
}
