using Microsoft.EntityFrameworkCore;
using RatioMusic.Application.Abstracts;
using RatioMusic.Domain.Entities;
using RatioMusic.Infrastructure.Contexts;

namespace RatioMusic.Infrastructure.Implements
{
    public class ArtistRepository : IArtistRepository
    {        
        private readonly RatioMusicContext _context;

        public ArtistRepository(RatioMusicContext context)
        {
            _context = context;
        }

        public async Task<int> CreateArtistAsync(Artist Artist)
        {
            try
            {
                await _context.Artists.AddAsync(Artist);
                await _context.SaveChangesAsync();

                return Artist.Id;
            }catch (Exception ex)
            {
                // log error
                return 0;
            }
        }

        public async Task<bool> DeleteArtistAsync(int id)
        {
            var Artist = await GetByIdAsync(id);
            if (Artist == null) return false;
            
            _context.Artists.Remove(Artist);
            await _context.SaveChangesAsync();

            return true;
        }

        public IEnumerable<Artist> GetAll(bool isTracking = false)
        {            
            return isTracking ? _context.Artists : _context.Artists.AsNoTracking();
        }

        public async Task<Artist>? GetByIdAsync(int id, bool isTracking = true)
        {
            if(id == 0) return null;

            var Artist = isTracking ? await _context.Artists.FirstOrDefaultAsync(s => s.Id == id)
                : await _context.Artists.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (Artist == null) return null;

            return Artist;
        }

        public async Task<bool> UpdateArtistAsync(Artist Artist)
        {
            try
            {
                if (Artist == null) return false;

                _context.Artists.Update(Artist);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // log error
                return false;
            }
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }        
    }
}
