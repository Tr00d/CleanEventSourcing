using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CleanEventSourcing.Persistence
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<ItemSummary> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ItemSummaryConfiguration());
        }
    }
}