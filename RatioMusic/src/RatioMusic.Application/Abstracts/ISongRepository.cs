using RatioMusic.Domain.Entities;

namespace RatioMusic.Application.Abstracts
{
    public interface ISongRepository
    {
        IEnumerable<Song> GetAll(bool isTracking = false);
        Task<Song>? GetByIdAsync(int id, bool isTracking = true);

        Task<int> CreateSongAsync(Song song);
        Task<bool> UpdateSongAsync(Song song);
        Task<bool> DeleteSongAsync(int id);
        void SaveChange();
        Task SaveChangeAsync();
    }
}
