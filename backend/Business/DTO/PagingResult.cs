using System.Collections.Generic;

namespace SmsChallengeBackend.Business.DTO
{
    public class PagingResult<T>
    {
        public List<T> Data { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }    
    }
}
