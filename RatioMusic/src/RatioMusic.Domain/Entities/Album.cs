namespace RatioMusic.Domain.Entities
{
    public class Album : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        public int AlbumOwner { get; set; }
        public DateTime Release { get; set; }        
    }
}