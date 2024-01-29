using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RatioMusic.Application.Abstracts;
using RatioMusic.Application.ViewModels;
using RatioMusic.Domain.Entities;

namespace RatioMusic.Application.Services
{
    public class SongService : ISongService
    {
        private readonly IBaseRepository<Song> _songRepository;
        private readonly IMapper _mapper;

        public SongService(IBaseRepository<Song> songRepository, IMapper mapper)
        {
            _songRepository = songRepository;
            _mapper = mapper;
        }

        public async Task<SongViewModel> CreateSongAsync(SongApiRequest newSongRequest)
        {
            int songId = await _songRepository.CreateAsync(_mapper.Map<Song>(newSongRequest));
            if (songId == 0) return null;

            var res = _mapper.Map<SongViewModel>(newSongRequest);
            res.Song.Id = songId; 

            return res;            
        }

        public async Task<bool> DeleteSong(int id)
        {
            return await _songRepository.DeleteAsync(id);
        }

        public async Task<List<Song>> GetAllSongsAsync()
        {            
            return await _songRepository.GetAll().AsQueryable().ToListAsync();
        }

        public async Task<Song?> GetSongById(int id, bool isTracking = true)
        {
            if(id == 0) return null;

            return await _songRepository.GetByIdAsync(id, isTracking);
        }

        public async Task<bool> UpdateSongAsync(SongApiRequest song)
        {
            var res = await _songRepository.UpdateAsync(_mapper.Map<Song>(song));

            return res;
        }
    }
}
