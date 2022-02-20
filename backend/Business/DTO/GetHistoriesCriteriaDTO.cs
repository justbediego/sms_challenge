using System;

namespace SmsChallengeBackend.Business.DTO
{
    public class GetHistoriesCriteriaDTO
    {
        public string Keyword { get; set; }
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public SortField? SortField { get; set; }
        public bool? IsAscending { get; set; }
    }
}
