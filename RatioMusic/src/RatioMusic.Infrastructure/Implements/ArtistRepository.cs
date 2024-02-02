using RatioMusic.Application.Abstracts;
using RatioMusic.Domain.Entities;
using RatioMusic.Infrastructure.Contexts;

namespace RatioMusic.Infrastructure.Implements
{
    public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(RatioMusicContext context) : base(context)
        {
        }
    }
}
