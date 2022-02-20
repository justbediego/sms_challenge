using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmsChallengeBackend.DataAccess.Model
{
    public class HistoryData
    {
        [Key]
        [Column("id")]
        // using postgres internal
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("city")]
        [MaxLength(100)]
        public string City { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("price")]
        public double Price { get; set; }

        [Column("status", TypeName = "varchar(10)")]
        [MaxLength(10)]
        public StatusCode Status { get; set; }

        [Column("color")]
        [MaxLength(10)]
        public string Color { get; set; }
    }
}
