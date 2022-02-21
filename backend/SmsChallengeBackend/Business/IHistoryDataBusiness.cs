using SmsChallengeBackend.Business.DTO;
using SmsChallengeBackend.Business.Exception;
using SmsChallengeBackend.DataAccess;
using SmsChallengeBackend.DataAccess.Model;
using System;
using System.Linq;

namespace SmsChallengeBackend.Business
{
    public interface IHistoryDataBusiness
    {
        long CreateHistoryData(ModifyHistoryDataDTO model);

        void UpdateHistoryData(long id, ModifyHistoryDataDTO model);

        void DeleteHistoryData(long id);

        HistoryDataDTO GetHistoryData(long id);

        PagingResult<HistoryDataDTO> GetHistories(GetHistoriesCriteriaDTO criteria);
    }
}
