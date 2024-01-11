using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RatioMusic.Application.Abstracts;
using RatioMusic.Application.ViewModels;
using RatioMusic.Domain.Entities;

namespace RatioMusic.Application.Services
{
    public class ArtistService : IArtistService
    {        
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public ArtistService(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }
        public async Task<List<Artist>> GetArtistsAsync()
        {
            return await _artistRepository.GetAll().AsQueryable().ToListAsync();
        }

        public async Task<Artist?> GetArtistAsync(int id, bool isTracking = true)
        {
            if (id == 0) return null;

            return await _artistRepository.GetByIdAsync(id, isTracking);
        }

        public async Task<ArtistViewModel> CreateArtistAsync(ArtistApiRequest newArtistRequest)
        {
            int ArtistId = await _artistRepository.CreateArtistAsync(_mapper.Map<Artist>(newArtistRequest));
            if (ArtistId == 0) return null;

            var res = _mapper.Map<ArtistViewModel>(newArtistRequest);
            res.Artist.Id = ArtistId;

            return res;
        }
        
        public async Task<bool> UpdateArtistAsync(ArtistApiRequest Artist)
        {
            var res = await _artistRepository.UpdateArtistAsync(_mapper.Map<Artist>(Artist));

            return res;
        }

        public async Task<bool> DeleteArtistAsync(int id)
        {
            return await _artistRepository.DeleteArtistAsync(id);
        }        
    }
}
