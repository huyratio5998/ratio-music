using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RatioMusic.Application.Abstracts;
using RatioMusic.Application.ViewModels;
using RatioMusic.Domain.Entities;

namespace RatioMusic.Application.Services
{
    public class ArtistService : IArtistService
    {        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArtistService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Artist>> GetArtistsAsync()
        {
              return await _unitOfWork.ArtistRepository.GetAll().AsQueryable().ToListAsync();
        }

        public async Task<Artist?> GetArtistAsync(int id, bool isTracking = true)
        {
            if (id == 0) return null;

            return await _unitOfWork.ArtistRepository.GetByIdAsync(id, isTracking);
        }

        public async Task<ArtistViewModel> CreateArtistAsync(ArtistApiRequest newArtistRequest)
        {
            try
            {
                var artistObj = await _unitOfWork.ArtistRepository.CreateAsync(_mapper.Map<Artist>(newArtistRequest));
                await _unitOfWork.SaveAsync();

                if (artistObj == null || artistObj.Id == 0) return new ArtistViewModel();
                var res = _mapper.Map<ArtistViewModel>(newArtistRequest);
                res.Artist.Id = artistObj.Id;

                return res;
            }
            catch (Exception ex)
            {
                return new ArtistViewModel();
            }
        }
        
        public async Task<bool> UpdateArtistAsync(ArtistApiRequest Artist)
        {
            try
            {
                var res = _unitOfWork.ArtistRepository.Update(_mapper.Map<Artist>(Artist));
                if (!res) return false;

                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteArtistAsync(int id)
        {
            try
            {
                var res = await _unitOfWork.ArtistRepository.DeleteAsync(id);
                if(!res) return false;

                await _unitOfWork.SaveAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }            
        }        
    }
}
