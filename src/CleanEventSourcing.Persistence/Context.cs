using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

#pragma warning disable 8618

namespace CleanEventSourcing.Persistence
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<ItemSummary> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ItemSummaryConfiguration());
        }
    }
}