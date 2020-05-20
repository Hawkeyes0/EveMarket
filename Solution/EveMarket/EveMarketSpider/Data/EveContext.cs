using System.Threading.Tasks;
using EveMarketEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EveMarketSpider.Data
{
    public class EveContext : DbContext
    {
        public DbSet<MarketGroup> MarketGroups { get; set; }

        public DbSet<Type> Types { get; set; }

        public DbSet<Attribute> Attributes { get; set; }

        public DbSet<Effect> Effects { get; set; }

        public DbSet<Modifier> Modifiers { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Constellation> Constellations { get; set; }

        public DbSet<EveMarketEntities.System> Systems { get; set; }

        public DbSet<Station> Stations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MarketGroup>().HasKey(e => e.MarketGroupId);
            builder.Entity<TypeAttribute>().HasKey(e => new { e.TypeId, e.AttributeId });
            builder.Entity<TypeEffect>().HasKey(e => new { e.TypeId, e.EffectId });
            builder.Entity<Type>().HasKey(e => e.TypeId);
            builder.Entity<Type>().HasMany(e => e.DogmaAttributes);
            builder.Entity<Type>().HasMany(e => e.DogmaEffects);
            builder.Entity<Attribute>().HasKey(e => e.AttributeId);
            builder.Entity<Effect>().HasKey(e => e.EffectId);
            builder.Entity<Modifier>().HasKey(e => e.Func);
            builder.Entity<Region>().HasKey(e => e.RegionId);
            builder.Entity<Constellation>().HasKey(e => e.ConstellationId);
            builder.Entity<EveMarketEntities.System>().HasKey(e => e.SystemId);
            builder.Entity<Station>().HasKey(e => e.StationId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=../Eve.sqlite");
        }

        public async Task<EntityEntry<MarketGroup>> AddOrUpdateAsync(MarketGroup entity)
        {
            var exists = await FindAsync<MarketGroup>(entity.MarketGroupId);
            if (exists == null)
            {
                return Add(entity);
            }
            else
            {
                Entry(exists).CurrentValues.SetValues(entity);
                return Entry(entity);
            }
        }

        public async Task<EntityEntry<Type>> AddOrUpdateAsync(Type entity)
        {
            var exists = await FindAsync<Type>(entity.TypeId);
            if (exists == null)
            {
                return Add(entity);
            }
            else
            {
                Entry(exists).CurrentValues.SetValues(entity);
                return Entry(entity);
            }
        }

        public async Task<EntityEntry<TypeAttribute>> AddOrUpdateAsync(TypeAttribute entity)
        {
            var exists = await FindAsync<TypeAttribute>(entity.TypeId, entity.AttributeId);
            if (exists == null)
            {
                return Add(entity);
            }
            else
            {
                Entry(exists).CurrentValues.SetValues(entity);
                return Entry(entity);
            }
        }

        public async Task<EntityEntry<TypeEffect>> AddOrUpdateAsync(TypeEffect entity)
        {
            var exists = await FindAsync<TypeEffect>(entity.TypeId, entity.EffectId);
            if (exists == null)
            {
                return Add(entity);
            }
            else
            {
                Entry(exists).CurrentValues.SetValues(entity);
                return Entry(entity);
            }
        }

        public async Task<EntityEntry<Region>> AddOrUpdateAsync(Region entity)
        {
            var exists = await FindAsync<Region>(entity.RegionId);
            if (exists == null)
            {
                return Add(entity);
            }
            else
            {
                Entry(exists).CurrentValues.SetValues(entity);
                return Entry(entity);
            }
        }

        public async Task<EntityEntry<Constellation>> AddOrUpdateAsync(Constellation entity)
        {
            var exists = await FindAsync<Constellation>(entity.ConstellationId);
            if (exists == null)
            {
                return Add(entity);
            }
            else
            {
                Entry(exists).CurrentValues.SetValues(entity);
                return Entry(entity);
            }
        }

        public async Task<EntityEntry<EveMarketEntities.System>> AddOrUpdateAsync(EveMarketEntities.System entity)
        {
            var exists = await FindAsync<EveMarketEntities.System>(entity.SystemId);
            if (exists == null)
            {
                return Add(entity);
            }
            else
            {
                Entry(exists).CurrentValues.SetValues(entity);
                return Entry(entity);
            }
        }

        public async Task<EntityEntry<Station>> AddOrUpdateAsync(Station entity)
        {
            var exists = await FindAsync<Station>(entity.StationId);
            if (exists == null)
            {
                return Add(entity);
            }
            else
            {
                Entry(exists).CurrentValues.SetValues(entity);
                return Entry(entity);
            }
        }
    }
}