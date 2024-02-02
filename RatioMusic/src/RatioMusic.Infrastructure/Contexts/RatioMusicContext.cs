using Microsoft.EntityFrameworkCore;
using RatioMusic.Domain.Entities;

namespace RatioMusic.Infrastructure.Contexts
{
    public class RatioMusicContext : DbContext
    {
        public RatioMusicContext(DbContextOptions<RatioMusicContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>(entity =>
            {
                entity.HasIndex(x => new { x.Name, x.DisplayName }).IsClustered(false);
            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasIndex(x => x.Name).IsClustered(false);
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.HasIndex(x => x.Name).IsClustered(false);
            });
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<SongArtist> SongArtists { get; set; }        
    }
}
