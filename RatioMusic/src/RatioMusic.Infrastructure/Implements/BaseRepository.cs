using Microsoft.EntityFrameworkCore;
using RatioMusic.Application.Abstracts;
using RatioMusic.Domain.Entities;
using RatioMusic.Infrastructure.Contexts;

namespace RatioMusic.Infrastructure.Implements
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly RatioMusicContext _context;

        public BaseRepository(RatioMusicContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                // log error
                return 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var T = await GetByIdAsync(id);
            if (T == null) return false;

            _context.Set<T>().Remove(T);
            await _context.SaveChangesAsync();

            return true;
        }

        public IEnumerable<T> GetAll(bool isTracking = false)
        {
            return isTracking ? _context.Set<T>() : _context.Set<T>().AsNoTracking();
        }

        public async Task<T>? GetByIdAsync(int id, bool isTracking = true)
        {
            if (id == 0) return null;

            var T = isTracking ? await _context.Set<T>().FirstOrDefaultAsync(s => s.Id == id)
                : await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (T == null) return null;

            return T;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                if (entity == null) return false;

                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // log error
                return false;
            }
        }
    }
}
