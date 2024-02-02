using Microsoft.EntityFrameworkCore;
using RatioMusic.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatioMusic.Application.ViewModels
{
    public class PagedResponse<T> : BaseResponse
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public PagedResponse(List<T> items, int pageIndex, int pageSize, int totalRecords, int totalPages)
        {
            Items = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = totalPages;
        }

        public static async Task<PagedResponse<T>> CreateAsync<T>(IQueryable<T> list, int pageNumber, int pageSize)
        {
            var totalItems = await list.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var pagingItems = await list.ToCustomPaging<T>(pageNumber, pageSize).ToListAsync();

            return new PagedResponse<T>(pagingItems, pageNumber, pageSize, totalItems, totalPages);
        }
    }
}
