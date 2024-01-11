namespace RatioMusic.Domain.Entities
{
    public class Song : BaseEntity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Images { get; set; }
        public string Resource { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Lyrics { get; set; }
        public int ViewNumber { get; set; } = 0;
        public int? AlbumId { get; set; }        

        public List<SongArtist> SongArtists { get; set; }
    }
}
