using Microsoft.EntityFrameworkCore;
using RatioMusic.Application.Abstracts;
using RatioMusic.Domain.Entities;
using RatioMusic.Infrastructure.Contexts;

namespace RatioMusic.Infrastructure.Implements
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly RatioMusicContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(RatioMusicContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.ModifiedDate = DateTime.UtcNow;
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;
            
            _dbSet.Remove(entity);            

            return true;
        }

        public IEnumerable<T> GetAll(bool isTracking = false)
        {
            return isTracking ? _dbSet : _dbSet.AsNoTracking();
        }

        public async Task<T?> GetByIdAsync(int id, bool isTracking = true)
        {
            if (id == 0) return null!;

            var entity = isTracking ? await _dbSet.FirstOrDefaultAsync(s => s.Id == id)
                : await _dbSet.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (entity == null) return null!;

            return entity;
        }

        public bool Update(T entity)
        {
            if (entity == null) return false;

            entity.ModifiedDate = DateTime.UtcNow;
            _dbSet.Update(entity);
            return true;
        }
    }
}
