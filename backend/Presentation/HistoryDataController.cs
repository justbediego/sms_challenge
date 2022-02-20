using Microsoft.AspNetCore.Mvc;
using SmsChallengeBackend.Business;
using SmsChallengeBackend.Business.DTO;
using System;

namespace SmsChallengeBackend.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryDataController : ControllerBase
    {

        private readonly IHistoryDataBusiness historyDataBusiness;

        public HistoryDataController(IHistoryDataBusiness historyDataBusiness)
        {
            this.historyDataBusiness = historyDataBusiness;
        }

        [HttpPost]
        public void CreateHistoryData(ModifyHistoryDataDTO model)
        {
            historyDataBusiness.CreateHistoryData(model);
        }

        [HttpPut]
        [Route("{id:long}")]
        public void UpdateHistoryData(long id, [FromBody] ModifyHistoryDataDTO model)
        {
            historyDataBusiness.UpdateHistoryData(id, model);
        }

        [HttpDelete]
        [Route("{id:long}")]
        public void DeleteHistoryData(long id)
        {
            historyDataBusiness.DeleteHistoryData(id);
        }

        [HttpGet]
        [Route("{id:long}")]
        public HistoryDataDTO GetHistoryData(long id)
        {
            return historyDataBusiness.GetHistoryData(id);
        }

        // all parameters are optionals to beautify the URL
        [HttpGet]
        public PagingResult<HistoryDataDTO> GetHistories(string keyword = null, int? pageSize = null, int? pageIndex = null, DateTime? fromData = null, DateTime? toDate = null, SortField? sortField = null, bool? isAscending = null)
        {
            return historyDataBusiness.GetHistories(new GetHistoriesCriteriaDTO
            {
                keyword = keyword,
                pageSize = pageSize,
                pageIndex = pageIndex,
                fromData = fromData,
                toDate = toDate,
                sortField = sortField,
                isAscending = isAscending
            });
        }
    }
}
