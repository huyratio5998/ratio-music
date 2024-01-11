using RatioMusic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatioMusic.Application.Abstracts
{
    public interface IArtistRepository
    {
        IEnumerable<Artist> GetAll(bool isTracking = false);
        Task<Artist>? GetByIdAsync(int id, bool isTracking = true);

        Task<int> CreateArtistAsync(Artist Artist);
        Task<bool> UpdateArtistAsync(Artist Artist);
        Task<bool> DeleteArtistAsync(int id);
        void SaveChange();
        Task SaveChangeAsync();
    }
}
