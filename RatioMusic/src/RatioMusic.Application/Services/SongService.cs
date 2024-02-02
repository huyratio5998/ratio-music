using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RatioMusic.Application.Abstracts;
using RatioMusic.Application.ViewModels;
using RatioMusic.Domain.Entities;

namespace RatioMusic.Application.Services
{
    public class SongService : ISongService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SongService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<SongViewModel> CreateSongAsync(SongApiRequest newSongRequest)
        {
            try
            {
                await _unitOfWork.CreateTransaction();

                var songObj = await _unitOfWork.SongRepository.CreateAsync(_mapper.Map<Song>(newSongRequest));

                await _unitOfWork.Save();
                await _unitOfWork.Commit();
                
                if (songObj.Id == 0) return new SongViewModel();

                var res = _mapper.Map<SongViewModel>(newSongRequest);
                res.Song.Id = songObj.Id;

                return res;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return new SongViewModel();
            }                    
        }

        public async Task<bool> DeleteSong(int id)
        {
            return await _unitOfWork.SongRepository.DeleteAsync(id);
        }

        public async Task<List<Song>> GetAllSongsAsync()
        {            
            return await _unitOfWork.SongRepository.GetAll().AsQueryable().ToListAsync();
        }

        public async Task<Song?> GetSongById(int id, bool isTracking = true)
        {
            if(id == 0) return null;

            return await _unitOfWork.SongRepository.GetByIdAsync(id, isTracking);
        }

        public async Task<bool> UpdateSongAsync(SongApiRequest song)
        {
            var res = await _unitOfWork.SongRepository.UpdateAsync(_mapper.Map<Song>(song));

            return res;
        }
    }
}
