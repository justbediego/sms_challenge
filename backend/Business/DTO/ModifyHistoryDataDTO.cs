using DataAnnotationsExtensions;
using SmsChallengeBackend.DataAccess.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace SmsChallengeBackend.Business.DTO
{
    public class ModifyHistoryDataDTO
    {
        [Required]
        public string City { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        [Min(0)]
        public double? Price { get; set; }

        [Required]
        public StatusCode? Status { get; set; }

        [Required]
        public string Color { get; set; }
    }
}
