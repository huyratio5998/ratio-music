using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RatioMusic.Application.Abstracts;
using RatioMusic.Application.Constants;
using RatioMusic.Application.ViewModels;
using RatioMusic.Domain.Entities;
using System;

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
                var songObj = await _unitOfWork.SongRepository.CreateAsync(_mapper.Map<Song>(newSongRequest));
                await _unitOfWork.SaveAsync();                
                
                if (songObj.Id == 0) return new SongViewModel();

                var res = _mapper.Map<SongViewModel>(newSongRequest);
                res.Song.Id = songObj.Id;

                return res;
            }
            catch (Exception ex)
            {                
                return new SongViewModel();
            }                    
        }

        public async Task<bool> DeleteSong(int id)
        {
            try
            {
                var res = await _unitOfWork.SongRepository.DeleteAsync(id);
                if (!res) return false;

                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }            
        }

        public async Task<PagedResponse<SongViewModel>?> GetAllSongsAsync(SongQueryParams queryParams)
        {
            var items = _unitOfWork.SongRepository.GetAll()
                        .AsQueryable();

            // filter
            if (!string.IsNullOrWhiteSpace(queryParams.SearchText)) items = items.Where(x => x.Name.Contains(queryParams.SearchText) || x.DisplayName.Contains(queryParams.SearchText));

            // order
            var orderCondition = queryParams.OrderBy;
            if (orderCondition != null)
            {
                if (orderCondition == OrderType.Asc) items = items.OrderBy(x => x.Id);
                else if (orderCondition == OrderType.Desc) items = items.OrderByDescending(y => y.Id);
            }

            // paging
            queryParams.PageNumber = queryParams.PageNumber <= 0 ? CommonConstant.PageIndexDefault : queryParams.PageNumber;
            queryParams.PageSize = queryParams.PageSize <= 0 ? CommonConstant.PageSizeDefault : queryParams.PageSize;

            var songs = await PagedResponse<Song>.CreateAsync(items, queryParams.PageNumber, queryParams.PageSize);

            var result = _mapper.Map<PagedResponse<SongViewModel>>(songs);
            return result;            
        }

        public async Task<Song?> GetSongById(int id, bool isTracking = true)
        {
            if(id == 0) return null;

            return await _unitOfWork.SongRepository.GetByIdAsync(id, isTracking);
        }

        public async Task<bool> UpdateSongAsync(SongApiRequest song)
        {
            try
            {
                var res = _unitOfWork.SongRepository.Update(_mapper.Map<Song>(song));
                if (!res) return false;

                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }           
        }
    }
}
