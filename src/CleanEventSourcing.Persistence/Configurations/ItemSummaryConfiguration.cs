using CleanEventSourcing.Domain.Items;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanEventSourcing.Persistence.Configurations
{
    public class ItemSummaryConfiguration: IEntityTypeConfiguration<ItemSummary>
    {
        public void Configure(EntityTypeBuilder<ItemSummary> builder)
        {
            builder.HasKey(item => item.Id);
        }
    }
}