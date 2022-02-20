using System;

namespace SmsChallengeBackend.Business.DTO
{
    public class GetHistoriesCriteriaDTO
    {
        public string keyword;
        public int? pageSize;
        public int? pageIndex;
        public DateTime? fromDate;
        public DateTime? toDate;
        public SortField? sortField;
        public bool? isAscending;
    }
}
