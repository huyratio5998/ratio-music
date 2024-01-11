using RatioMusic.Domain.Enums;

namespace RatioMusic.Domain.Entities
{
    public class SongArtist : BaseEntity
    {        
        public ProduceRole RoleInSong { get; set; }                        

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
