using EveMarketEntities;
using Microsoft.EntityFrameworkCore;

namespace EveMarketSpider.Data
{
    public class EveContext : DbContext
    {
        public DbSet<MarketGroup> MarketGroups {get;set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MarketGroup>().HasKey(e => e.MarketGroupId);
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=../Eve.sqlite");
        }
    }
}