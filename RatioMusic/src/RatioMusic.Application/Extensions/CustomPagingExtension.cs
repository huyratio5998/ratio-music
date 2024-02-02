using RatioMusic.Application.Constants;

namespace RatioMusic.Application.Extensions
{
    public static class CustomPagingExtension
    {
        public static IQueryable<T> ToCustomPaging<T>(this IQueryable<T> list, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0) pageNumber = CommonConstant.PageIndexDefault;
            if(pageSize <= 0) pageSize = CommonConstant.PageSizeDefault;

            list = list.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return list;
        }        
    }
}
