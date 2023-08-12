using BeFit.Data.Configurations;
using BeFit.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Data
{
    public class BeFitDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly bool seedDb;

        public BeFitDbContext(DbContextOptions<BeFitDbContext> options, bool seedDb = true)
            : base(options)
        {
            this.seedDb = seedDb;
        }

        public DbSet<Coach> Coaches { get; set; } = null!;
        public DbSet<CoachCategory> CoachCategories { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<EventCategory> EventCategories { get; set; } = null!;
        public DbSet<EventClient> EventClients { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<EventClient>()
                .HasKey(ec => new { ec.ClientId, ec.EventId });

            builder
                .Entity<EventClient>()
                .HasOne(ec => ec.Event)
                .WithMany(ec => ec.EventClients)
                .OnDelete(DeleteBehavior.NoAction);

			builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
			builder.ApplyConfiguration(new EventEntityConfiguration());

            if (this.seedDb)
            {
                builder.ApplyConfiguration(new SeedApplicationUserEntityConfiguration());
                builder.ApplyConfiguration(new SeedEventsCategoriesEntityConfiguration());
                builder.ApplyConfiguration(new SeedCoachesCategoriesEntityConfiguration());
                builder.ApplyConfiguration(new SeedCoachesEntityConfiguration());
                builder.ApplyConfiguration(new SeedEventsEntityConfiguration());
                builder.ApplyConfiguration(new SeedEventsClientsEntityConfiguration());
            }

			base.OnModelCreating(builder);
        }
    }
}