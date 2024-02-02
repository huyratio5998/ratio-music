using RatioMusic.Application.Abstracts;
using RatioMusic.Domain.Entities;
using RatioMusic.Infrastructure.Contexts;

namespace RatioMusic.Infrastructure.Implements
{
    public class SongRepository : BaseRepository<Song>, ISongRepository
    {
        public SongRepository(RatioMusicContext context) : base(context)
        {
        }
    }
}
