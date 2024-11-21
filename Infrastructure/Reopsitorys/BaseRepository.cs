using Domin.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProjectDemo.Data;

namespace Infrastructure.Reopsitorys
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _Db;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext Db)
        {
            _Db = Db;
            _dbSet = _Db.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _Db.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }

}
