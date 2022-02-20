using Microsoft.EntityFrameworkCore;
using SmsChallengeBackend.DataAccess.Model;

namespace SmsChallengeBackend.DataAccess
{
    public class SmsDbContext : DbContext
    {
        public SmsDbContext(DbContextOptions<SmsDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<HistoryData> Histories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseSerialColumns()
                .Entity<HistoryData>().ToTable("history_data");
        }
    }
}
