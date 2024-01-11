using RatioMusic.Application.ViewModels;
using RatioMusic.Domain.Entities;

namespace RatioMusic.Application.Services
{
    public interface ISongService
    {
        Task<List<Song>> GetAllSongsAsync();
        Task<Song> GetSongById(int id, bool isTracking = true);
        
        Task<SongViewModel> CreateSongAsync(SongApiRequest newSong);
        Task<bool> UpdateSongAsync(SongApiRequest song);
        Task<bool> DeleteSong(int id);
    }
}
