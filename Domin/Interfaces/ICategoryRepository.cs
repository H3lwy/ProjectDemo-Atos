using ProjectDemo.Models;

namespace Domin.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryAsync(int id);
    }
}
