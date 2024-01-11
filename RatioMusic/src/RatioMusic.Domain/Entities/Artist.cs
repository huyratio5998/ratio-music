using RatioMusic.Domain.Enums;

namespace RatioMusic.Domain.Entities
{
    public class Artist : BaseEntity
    {
        public string Images { get; set; }
        public string Name { get; set; }
        public string ArtistName { get; set; }
        public int Age { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime DebutDate { get; set; }
        public ProduceRole ProduceRole { get; set; }

        public List<SongArtist> SongArtists { get; set; }
    }
}