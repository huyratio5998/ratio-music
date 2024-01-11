using RatioMusic.Domain.Entities;
using RatioMusic.Domain.Enums;

namespace RatioMusic.Application.ViewModels
{
    public class ArtistApiRequest
    {
        public int Id { get; set; }
        public string Images { get; set; }
        public string Name { get; set; }
        public string ArtistName { get; set; }
        public int Age { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime DebutDate { get; set; }
        public ProduceRole ProduceRole { get; set; }        
    }
}