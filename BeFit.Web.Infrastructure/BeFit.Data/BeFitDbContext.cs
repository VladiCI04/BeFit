using BeFit.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BeFit.Data
{
    public class BeFitDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public BeFitDbContext(DbContextOptions<BeFitDbContext> options)
            : base(options)
        {

        }

        public DbSet<Coach> Coaches { get; set; } = null!;
        public DbSet<CoachCategory> CoachCategories { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<EventCategory> EventCategories { get; set; } = null!;
        public DbSet<EventClient> EventClients { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(BeFitDbContext)) ?? 
                                      Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(configAssembly);

            builder
                .Entity<EventClient>()
                .HasKey(ec => new { ec.ClientId, ec.EventId });

            builder
                .Entity<EventClient>()
                .HasOne(ec => ec.Event)
                .WithMany(ec => ec.EventClients)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }
    }
}