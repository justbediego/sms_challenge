using Microsoft.EntityFrameworkCore;
using Moq;
using SmsChallengeBackend.Business;
using SmsChallengeBackend.Business.DTO;
using SmsChallengeBackend.DataAccess;
using SmsChallengeBackend.DataAccess.Model;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using SmsChallengeBackend.Business.Exception;
using System;

namespace SmsChallengeBackendTest
{
    public class HistoryDataBusinessTest
    {
        private class SmsDbContextMock : SmsDbContext
        {
            public SmsDbContextMock() : base(new DbContextOptions<SmsDbContext>())
            {
                var mockHistoryDataSet = new Mock<DbSet<HistoryData>>();
                var list = new List<HistoryData>()
                {
                    new HistoryData { Id = 1, City = "Test1", EndDate = new DateTime(2022, 12, 15), StartDate = new DateTime(2022, 12, 1), Color = "#ffffff", Price = 200, Status = StatusCode.Yearly},
                    new HistoryData { Id = 2, City = "Test2", EndDate = new DateTime(2022, 12, 20), StartDate = new DateTime(2022, 12, 5), Color = "#aaaaaa", Price = 100, Status = StatusCode.Once},
                    new HistoryData { Id = 2, City = "DiffernetCity", EndDate = new DateTime(2022, 12, 30), StartDate = new DateTime(2022, 12, 10), Color = "#bbbbbb", Price = 50, Status = StatusCode.Seldom},
                };
                var queryable = list.AsQueryable();
                mockHistoryDataSet.As<IQueryable<HistoryData>>().Setup(m => m.Provider).Returns(queryable.Provider);
                mockHistoryDataSet.As<IQueryable<HistoryData>>().Setup(m => m.Expression).Returns(queryable.Expression);
                mockHistoryDataSet.As<IQueryable<HistoryData>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
                mockHistoryDataSet.As<IQueryable<HistoryData>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
                mockHistoryDataSet.Setup(m => m.Remove(It.IsAny<HistoryData>())).Callback<HistoryData>((data) => list.Remove(data));
                mockHistoryDataSet.Setup(m => m.Add(It.IsAny<HistoryData>())).Callback<HistoryData>((data) => { data.Id = 100; list.Add(data); });
                Histories = mockHistoryDataSet.Object;
            }
            public override int SaveChanges()
            {
                //do nothing
                return 0;
            }
        }

        [Fact]
        public void Test_GetHistoryData()
        {
            // given
            var historyDataBusiness = new HistoryDataBusiness(new SmsDbContextMock());

            // when
            HistoryDataDTO result = historyDataBusiness.GetHistoryData(1);

            // then
            Assert.Equal("Test1", result.City);
        }

        [Fact]
        public void Test_GetHistoryData_NotFound()
        {
            // given
            var historyDataBusiness = new HistoryDataBusiness(new SmsDbContextMock());

            // then
            Assert.Throws<EntityNotFoundException>(() =>
            {
                HistoryDataDTO result = historyDataBusiness.GetHistoryData(10);
            });
        }

        [Fact]
        public void Test_DeleteHistoryData()
        {
            // given
            var historyDataBusiness = new HistoryDataBusiness(new SmsDbContextMock());


            // when
            historyDataBusiness.DeleteHistoryData(1);

            // then
            Assert.Throws<EntityNotFoundException>(() =>
            {
                HistoryDataDTO result = historyDataBusiness.GetHistoryData(1);
            });
        }

        [Fact]
        public void Test_DeleteHistoryData_NotFound()
        {
            // given
            var historyDataBusiness = new HistoryDataBusiness(new SmsDbContextMock());

            // then
            Assert.Throws<EntityNotFoundException>(() =>
            {
                historyDataBusiness.DeleteHistoryData(10);
            });
        }

        [Fact]
        public void Test_CreateHistoryData()
        {
            // given
            var historyDataBusiness = new HistoryDataBusiness(new SmsDbContextMock());

            // when
            var newID = historyDataBusiness.CreateHistoryData(new ModifyHistoryDataDTO
            {
                City = "TestNew",
                Color = "#ABCDEF",
                StartDate = (DateTime.Now).AddYears(-1),
                EndDate = DateTime.Now,
                Price = 50,
                Status = StatusCode.Monthly
            });
            var result = historyDataBusiness.GetHistoryData(newID);

            // then
            Assert.Equal("TestNew", result.City);
        }
        [Fact]
        public void Test_CreateHistoryData_BadEndDate()
        {
            // given
            var historyDataBusiness = new HistoryDataBusiness(new SmsDbContextMock());

            // then
            Assert.Throws<InvalidInputException>(() =>
            {
                historyDataBusiness.CreateHistoryData(new ModifyHistoryDataDTO
                {
                    City = "TestNew",
                    Color = "#ABCDEF",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddYears(-1),
                    Price = 50,
                    Status = StatusCode.Monthly
                });
            });
        }

        [Fact]
        public void Test_UpdateHistoryData()
        {
            // given
            var historyDataBusiness = new HistoryDataBusiness(new SmsDbContextMock());

            // then
            var init = historyDataBusiness.GetHistoryData(1);
            init.City = "ChangedCity";
            historyDataBusiness.UpdateHistoryData(1, init);
            var result = historyDataBusiness.GetHistoryData(1);

            // then
            Assert.Equal("ChangedCity", result.City);
        }

        [Fact]
        public void Test_UpdateHistoryData_BadEndDate()
        {
            // given
            var historyDataBusiness = new HistoryDataBusiness(new SmsDbContextMock());

            // then
            Assert.Throws<InvalidInputException>(() =>
            {
                historyDataBusiness.UpdateHistoryData(1, new ModifyHistoryDataDTO
                {
                    City = "TestNew",
                    Color = "#ABCDEF",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddYears(-1),
                    Price = 50,
                    Status = StatusCode.Monthly
                });
            });
        }

        [Fact]
        public void Test_GetHistories_All()
        {
            // given
            var historyDataBusiness = new HistoryDataBusiness(new SmsDbContextMock());

            // when
            var result = historyDataBusiness.GetHistories(new GetHistoriesCriteriaDTO
            {
                // no criteria
            });

            // then
            Assert.Equal(3, result.TotalCount);
            Assert.Equal(result.Data.Count, result.TotalCount);
        }

        [Fact]
        public void Test_GetHistories_DateRange()
        {
            // given
            var historyDataBusiness = new HistoryDataBusiness(new SmsDbContextMock());

            // when
            var result = historyDataBusiness.GetHistories(new GetHistoriesCriteriaDTO
            {
                FromDate = new DateTime(2022, 12, 1),
                ToDate = new DateTime(2022, 12, 5)
            });

            // then
            Assert.Equal(1, result.TotalCount);
            Assert.Equal("Test1", result.Data[0].City);
        }

        [Fact]
        public void Test_GetHistories_SortPriceAsc()
        {
            // given
            var historyDataBusiness = new HistoryDataBusiness(new SmsDbContextMock());

            // when
            var result = historyDataBusiness.GetHistories(new GetHistoriesCriteriaDTO
            {
                SortField=SortField.Price
            });

            // then
            Assert.Equal(50, result.Data[0].Price);
        }

        [Fact]
        public void Test_GetHistories_SortPriceDesc()
        {
            // given
            var historyDataBusiness = new HistoryDataBusiness(new SmsDbContextMock());

            // when
            var result = historyDataBusiness.GetHistories(new GetHistoriesCriteriaDTO
            {
                SortField = SortField.Price,
                IsAscending = false
            });

            // then
            Assert.Equal(200, result.Data[0].Price);
        }

        [Fact]
        public void Test_GetHistories_Keyword()
        {
            // given
            var historyDataBusiness = new HistoryDataBusiness(new SmsDbContextMock());

            // when
            var result = historyDataBusiness.GetHistories(new GetHistoriesCriteriaDTO
            {
                Keyword = "tEST"
            });

            // then
            Assert.Equal(2, result.TotalCount);
        }
    }
}
