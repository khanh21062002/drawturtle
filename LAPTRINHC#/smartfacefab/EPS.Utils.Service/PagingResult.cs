using System.Collections.Generic;

namespace EPS.Utils.Service
{
    public class PagingResult<TDto> 
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalRows { get; set; }
        public List<TDto> Data { get; set; }
    }
}
