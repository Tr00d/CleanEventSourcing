using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Domain.Items.Events;
using CleanEventSourcing.Persistence.ReadModel;
using FluentAssertions;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CleanEventSourcing.Persistence.Tests.ReadModel
{
    public class InMemoryReadServiceTest
    {
        private readonly Fixture fixture;
        private Context context;
        
        public InMemoryReadServiceTest()
        {
            this.fixture = new Fixture();
            DbContextOptions<Context> options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(this.fixture.Create<string>()).Options;
            this.context = new Context(options);
        }

        [Fact]
        public async Task HandleCreatedItemEvent_ShouldCreateItem()
        {
            CreatedItemEvent receivedEvent = this.fixture.Create<CreatedItemEvent>();
            InMemoryReadService service = new InMemoryReadService(this.context);
            await service.Handle(receivedEvent, this.fixture.Create<CancellationToken>());
            Option<ItemSummary> result = await service.GetItemAsync(receivedEvent.Id);
            result.IsSome.Should().Be(true);
            ItemSummary summary = result.IfNone(new ItemSummary());
            summary.Id.Should().Be(receivedEvent.Id);
            summary.Description.Should().Be(receivedEvent.Description.IfNone(string.Empty));
        }
    }
}