using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatioMusic.Application.ViewModels
{
    public class BaseQueryParams
    {
        public string? SearchText { get; set; } = string.Empty;
        public OrderType? OrderBy { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public enum OrderType
    {
        Asc = 0,
        Desc = 1,
    }
}
