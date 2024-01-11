using Microsoft.EntityFrameworkCore;
using RatioMusic.Domain.Entities;

namespace RatioMusic.Infrastructure.Contexts
{
    public class RatioMusicContext : DbContext
    {
        public RatioMusicContext(DbContextOptions<RatioMusicContext> options) : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<SongArtist> SongArtists { get; set; }        
    }
}
