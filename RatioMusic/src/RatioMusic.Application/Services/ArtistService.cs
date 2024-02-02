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
                await _unitOfWork.CreateTransaction();
                
                var artistObj = await _unitOfWork.ArtistRepository.CreateAsync(_mapper.Map<Artist>(newArtistRequest));
                
                await _unitOfWork.Save();
                await _unitOfWork.Commit();
                
                if (artistObj.Id == 0) return new ArtistViewModel();
                
                var res = _mapper.Map<ArtistViewModel>(newArtistRequest);
                res.Artist.Id = artistObj.Id;

                return res;
            }
            catch(Exception ex)
            {
                await _unitOfWork.Rollback();
                return new ArtistViewModel();
            }                        
        }
        
        public async Task<bool> UpdateArtistAsync(ArtistApiRequest Artist)
        {
            var res = await _unitOfWork.ArtistRepository.UpdateAsync(_mapper.Map<Artist>(Artist));

            return res;
        }

        public async Task<bool> DeleteArtistAsync(int id)
        {
            return await _unitOfWork.ArtistRepository.DeleteAsync(id);
        }        
    }
}
