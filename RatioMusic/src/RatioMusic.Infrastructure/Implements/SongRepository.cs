using Microsoft.EntityFrameworkCore;
using RatioMusic.Application.Abstracts;
using RatioMusic.Domain.Entities;
using RatioMusic.Infrastructure.Contexts;

namespace RatioMusic.Infrastructure.Implements
{
    public class SongRepository : ISongRepository
    {        
        private readonly RatioMusicContext _context;

        public SongRepository(RatioMusicContext context)
        {
            _context = context;
        }

        public async Task<int> CreateSongAsync(Song song)
        {
            try
            {
                await _context.Songs.AddAsync(song);
                await _context.SaveChangesAsync();

                return song.Id;
            }catch (Exception ex)
            {
                // log error
                return 0;
            }
        }

        public async Task<bool> DeleteSongAsync(int id)
        {
            var song = await GetByIdAsync(id);
            if (song == null) return false;
            
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return true;
        }

        public IEnumerable<Song> GetAll(bool isTracking = false)
        {            
            return isTracking ? _context.Songs : _context.Songs.AsNoTracking();
        }

        public async Task<Song>? GetByIdAsync(int id, bool isTracking = true)
        {
            if(id == 0) return null;

            var song = isTracking ? await _context.Songs.FirstOrDefaultAsync(s => s.Id == id)
                : await _context.Songs.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (song == null) return null;

            return song;
        }

        public async Task<bool> UpdateSongAsync(Song song)
        {
            try
            {
                if (song == null) return false;

                _context.Songs.Update(song);
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
