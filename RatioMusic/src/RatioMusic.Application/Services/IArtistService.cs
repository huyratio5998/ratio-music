using RatioMusic.Application.ViewModels;
using RatioMusic.Domain.Entities;

namespace RatioMusic.Application.Services
{
    public interface IArtistService
    {
        Task<List<Artist>> GetArtistsAsync();
        Task<Artist> GetArtistAsync(int id, bool isTracking = true);
        Task<ArtistViewModel> CreateArtistAsync(ArtistApiRequest artist);
        Task<bool> UpdateArtistAsync(ArtistApiRequest artist);
        Task<bool> DeleteArtistAsync(int id);
    }
}
