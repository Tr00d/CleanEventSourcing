using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Domain.Items.Events;
using CleanEventSourcing.Persistence.ReadModel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CleanEventSourcing.Persistence.Tests.ReadModel
{
    public class InMemoryReadServiceTest
    {
        private readonly Context context;
        private readonly Fixture fixture;

        public InMemoryReadServiceTest()
        {
            this.fixture = new Fixture();
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(this.fixture.Create<string>()).Options;
            this.context = new Context(options);
        }

        [Fact]
        public async Task HandleCreatedItemEvent_ShouldCreateItem()
        {
            var receivedEvent = this.fixture.Create<CreatedItemEvent>();
            var service = new InMemoryReadService(this.context);
            await service.Handle(receivedEvent, CancellationToken.None);
            var result = await service.GetItemAsync(receivedEvent.Id);
            result.IsSome.Should().Be(true);
            var summary = result.IfNone(new ItemSummary());
            summary.Id.Should().Be(receivedEvent.Id);
            summary.Description.Should().Be(receivedEvent.Description.IfNone(string.Empty));
        }

        [Fact]
        public async Task HandleUpdatedItemEvent_ShouldUpdateItem()
        {
            var receivedEvent = this.fixture.Create<UpdatedItemEvent>();
            await this.context.Items.AddAsync(
                new ItemSummary { Id = receivedEvent.Id, Description = this.fixture.Create<string>() },
                CancellationToken.None);
            await this.context.SaveChangesAsync(CancellationToken.None);
            var service = new InMemoryReadService(this.context);
            await service.Handle(receivedEvent, CancellationToken.None);
            var result = await service.GetItemAsync(receivedEvent.Id);
            result.IsSome.Should().Be(true);
            var summary = result.IfNone(new ItemSummary());
            summary.Description.Should().Be(receivedEvent.NewDescription.IfNone(string.Empty));
        }

        [Fact]
        public async Task HandleDeletedItemEvent_ShouldDeleteItem()
        {
            var receivedEvent = this.fixture.Create<DeletedItemEvent>();
            await this.context.Items.AddAsync(
                new ItemSummary { Id = receivedEvent.Id, Description = this.fixture.Create<string>() },
                CancellationToken.None);
            await this.context.SaveChangesAsync(CancellationToken.None);
            var service = new InMemoryReadService(this.context);
            await service.Handle(receivedEvent, CancellationToken.None);
            var result = await service.GetItemAsync(receivedEvent.Id);
            result.IsNone.Should().Be(true);
        }
    }
}