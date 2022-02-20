using SmsChallengeBackend.Business.DTO;
using SmsChallengeBackend.Business.Exception;
using SmsChallengeBackend.DataAccess;
using SmsChallengeBackend.DataAccess.Model;
using System;
using System.Linq;

namespace SmsChallengeBackend.Business
{
    public class HistoryDataBusiness : IHistoryDataBusiness
    {
        // the business is small enough not to require repository pattern
        // validations are done at DTO level as no logic validations are required
        private readonly SmsDbContext dbContext;

        private const int MaxPageSize = 200;

        public HistoryDataBusiness(SmsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public long CreateHistoryData(ModifyHistoryDataDTO model)
        {
            if (model.EndDate < model.StartDate)
            {
                throw new InvalidInputException("EndDate", "EndDate is before StartDate");
            }
            HistoryData newData = new HistoryData
            {
                City = model.City,
                Color = model.Color,
                EndDate = model.EndDate,
                Price = model.Price,
                StartDate = model.StartDate,
                Status = model.Status
            };
            dbContext.Histories.Add(newData);
            dbContext.SaveChanges();
            return newData.Id;
        }

        public void UpdateHistoryData(long id, ModifyHistoryDataDTO model)
        {
            HistoryData historyData = dbContext.Histories
                .Where(history => history.Id == id)
                .FirstOrDefault();
            if (historyData == null)
            {
                throw new EntityNotFoundException("HistoryData");
            }
            if (model.EndDate < model.StartDate)
            {
                throw new InvalidInputException("EndDate", "EndDate is before StartDate");
            }
            historyData.StartDate = model.StartDate;
            historyData.EndDate = model.EndDate;
            historyData.Status = model.Status;
            historyData.Color = model.Color;
            historyData.City = model.City;
            historyData.Price = model.Price;
            dbContext.Histories.Update(historyData);
            dbContext.SaveChanges();
        }

        public void DeleteHistoryData(long id)
        {
            HistoryData historyData = dbContext.Histories
                .Where(history => history.Id == id)
                .FirstOrDefault();
            if (historyData == null)
            {
                throw new EntityNotFoundException("HistoryData");
            }
            dbContext.Histories.Remove(historyData);
            dbContext.SaveChanges();
        }

        public HistoryDataDTO GetHistoryData(long id)
        {
            HistoryData historyData = dbContext.Histories
                .Where(history => history.Id == id)
                .FirstOrDefault();
            if (historyData == null)
            {
                throw new EntityNotFoundException("HistoryData");
            }
            return new HistoryDataDTO
            {
                Id = id,
                City = historyData.City,
                Price = historyData.Price,
                Color = historyData.Color,
                StartDate = historyData.StartDate,
                EndDate = historyData.EndDate,
                Status = historyData.Status,
            };
        }

        public PagingResult<HistoryDataDTO> GetHistories(GetHistoriesCriteriaDTO criteria)
        {
            IQueryable<HistoryData> histories = dbContext.Histories;
            if (criteria.Keyword != null)
            {
                histories = histories.Where(history => history.City.ToLower().Contains(criteria.Keyword.Trim().ToLower()));
            }
            if (criteria.FromDate != null)
            {
                // overlap of two ranges is applied to both their sides
                histories = histories.Where(history => history.StartDate >= criteria.FromDate || history.EndDate > criteria.FromDate);
            }

            if (criteria.ToDate != null)
            {
                // overlap of two ranges is applied to both their sides
                histories = histories.Where(history => history.StartDate < criteria.ToDate || history.EndDate <= criteria.ToDate);
            }

            if (criteria.SortField != null)
            {
                bool isAscending = criteria.IsAscending ?? true;
                histories = criteria.SortField switch
                {
                    SortField.Price => isAscending ? histories.OrderBy(hist => hist.Price) : histories.OrderByDescending(hist => hist.Price),
                    SortField.StartDate => isAscending ? histories.OrderBy(hist => hist.StartDate) : histories.OrderByDescending(hist => hist.StartDate),
                    SortField.EndDate => isAscending ? histories.OrderBy(hist => hist.EndDate) : histories.OrderByDescending(hist => hist.EndDate),
                    SortField.City => isAscending ? histories.OrderBy(hist => hist.City.ToLower()) : histories.OrderByDescending(hist => hist.City.ToLower()),
                    SortField.Status => isAscending ? histories.OrderBy(hist => hist.Status) : histories.OrderByDescending(hist => hist.Status),
                    SortField.Color => isAscending ? histories.OrderBy(hist => hist.Color.ToLower()) : histories.OrderByDescending(hist => hist.Color.ToLower()),
                    // id is by default
                    _ => isAscending ? histories.OrderBy(hist => hist.Id) : histories.OrderByDescending(hist => hist.Id),
                };
            }

            int pageSize = criteria.PageSize ?? MaxPageSize;
            int pageIndex = criteria.PageIndex ?? 1;

            if (pageSize > MaxPageSize || pageSize < 1)
            {
                throw new InvalidInputException("PageSize", "value is either negative or too large");
            }
            if (pageIndex < 1)
            {
                throw new InvalidInputException("PageIndex", "value is less than 1");
            }

            long totalCount = histories.Count();
            return new PagingResult<HistoryDataDTO>
            {
                Data = histories
                        .Skip(pageSize * (pageIndex - 1))
                        .Take(pageSize)
                        .Select(dbHistory => new HistoryDataDTO
                        {
                            Id = dbHistory.Id,
                            StartDate = dbHistory.StartDate,
                            EndDate = dbHistory.EndDate,
                            City = dbHistory.City,
                            Color = dbHistory.Color,
                            Price = dbHistory.Price,
                            Status = dbHistory.Status,
                        })
                        .ToList(),
                TotalCount = totalCount,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

    }
}
