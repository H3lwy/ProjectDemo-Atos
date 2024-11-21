using ProjectDemo.Models;

namespace Domin.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllWithCategoryAsync();
        Task<Product> GetByIdWithCategoryAsync(int id);
        Task<Product> GetProductAsync(int id);

    }
}
