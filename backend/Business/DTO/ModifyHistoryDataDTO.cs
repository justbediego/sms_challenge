using SmsChallengeBackend.DataAccess.Model;
using System;

namespace SmsChallengeBackend.Business.DTO
{
    public class ModifyHistoryDataDTO
    {
        public string City { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Price { get; set; }

        public StatusCode Status { get; set; }

        public string Color { get; set; }
    }
}
