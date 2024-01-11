using AutoMapper;
using RatioMusic.Application.ViewModels;
using RatioMusic.Domain.Entities;

namespace RatioMusic.Application.Mapper
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile() {

            CreateMap<SongApiRequest, Song>();
            CreateMap<SongApiRequest, SongViewModel>()
                .ForMember(des => des.Song, des => des.MapFrom(s => s));

            CreateMap<ArtistApiRequest, Artist>();
            CreateMap<ArtistApiRequest, ArtistViewModel>()
                .ForMember(des => des.Artist, des => des.MapFrom(s => s));
        }

    }
}
