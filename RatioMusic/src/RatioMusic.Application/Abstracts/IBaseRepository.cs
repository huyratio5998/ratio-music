using RatioMusic.Domain.Entities;

namespace RatioMusic.Application.Abstracts
{
    public interface IBaseRepository<T> where T: BaseEntity
    {
        IEnumerable<T> GetAll(bool isTracking = false);
        Task<T>? GetByIdAsync(int id, bool isTracking = true);

        Task<T> CreateAsync(T Artist);
        Task<bool> UpdateAsync(T Artist);
        Task<bool> DeleteAsync(int id);        
    }
}
