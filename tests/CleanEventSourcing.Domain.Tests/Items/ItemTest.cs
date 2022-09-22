using System;
using System.Linq;
using AutoFixture;
using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Domain.Items.Events;
using FluentAssertions;
using Xunit;

namespace CleanEventSourcing.Domain.Tests.Items
{
    public class ItemTest
    {
        private readonly Fixture fixture;

        public ItemTest()
        {
            this.fixture = new Fixture();
        }

        [Fact]
        public void Constructor_ShouldSetId()
        {
            var id = this.fixture.Create<Guid>();
            var item = new Item(id, this.fixture.Create<string>());
            item.Id.Should().Be(id);
        }

        [Fact]
        public void Constructor_ShouldSetDescription()
        {
            var description = this.fixture.Create<string>();
            var item = new Item(this.fixture.Create<Guid>(), description);
            item.Description.IsSome.Should().Be(true);
            item.Description.IfNone(string.Empty).Should().Be(description);
        }

        [Fact]
        public void GetIntegrationEvents_ShouldContainCreatedItemEvent_GivenItemIsCreated()
        {
            var id = this.fixture.Create<Guid>();
            var description = this.fixture.Create<string>();
            var item = new Item(id, description);
            var events = item.GetIntegrationEvents().IfNone(Enumerable.Empty<IIntegrationEvent>())
                .ToArray();
            events.First().Should().BeOfType<CreatedItemEvent>();
            var result = (CreatedItemEvent)events.First();
            result.Id.Should().Be(id);
            result.Description.IsSome.Should().Be(true);
            result.Description.IfNone(string.Empty).Should().Be(description);
        }

        [Fact]
        public void GetIntegrationEvents_ShouldContainUpdatedItemEvent_GivenItemIsUpdated()
        {
            var oldDescription = this.fixture.Create<string>();
            var description = this.fixture.Create<string>();
            var item = new Item(this.fixture.Create<Guid>(), oldDescription);
            item.Update(description);
            var events = item.GetIntegrationEvents().IfNone(Enumerable.Empty<IIntegrationEvent>())
                .ToArray();
            events.Any(integrationEvent => integrationEvent.GetType() == typeof(UpdatedItemEvent)).Should().Be(true);
            var result = (UpdatedItemEvent)events.First(integrationEvent =>
                integrationEvent.GetType() == typeof(UpdatedItemEvent));
            result.Id.Should().Be(item.Id);
            result.OldDescription.IfNone(string.Empty).Should().Be(oldDescription);
            result.NewDescription.IfNone(string.Empty).Should().Be(description);
        }

        [Fact]
        public void GetIntegrationEvents_ShouldContainDeletedItemEvent_GivenItemIsDeleted()
        {
            var item = new Item(this.fixture.Create<Guid>(), this.fixture.Create<string>());
            item.Delete();
            var events = item.GetIntegrationEvents().IfNone(Enumerable.Empty<IIntegrationEvent>())
                .ToArray();
            events.Any(integrationEvent => integrationEvent.GetType() == typeof(DeletedItemEvent)).Should().Be(true);
            var result = (DeletedItemEvent)events.First(integrationEvent =>
                integrationEvent.GetType() == typeof(DeletedItemEvent));
            result.Id.Should().Be(item.Id);
        }

        [Fact]
        public void Update_ShouldUpdateDescription()
        {
            var description = this.fixture.Create<string>();
            var item = new Item(this.fixture.Create<Guid>(), this.fixture.Create<string>());
            item.Update(description);
            item.Description.IfNone(string.Empty).Should().Be(description);
        }

        [Fact]
        public void Delete_ShouldDeleteItem()
        {
            var item = new Item(this.fixture.Create<Guid>(), this.fixture.Create<string>());
            item.Delete();
            item.IsDeleted.Should().Be(true);
        }

        [Fact]
        public void ApplyCreatedItemEvent_ShouldSetId_GivenAggregateIsSome()
        {
            var createdEvent = this.fixture.Create<CreatedItemEvent>();
            var aggregate = new Item(this.fixture.Create<Guid>(), this.fixture.Create<string>());
            aggregate.Apply(createdEvent);
            aggregate.Id.Should().Be(createdEvent.Id);
        }

        [Fact]
        public void ApplyCreatedItemEvent_ShouldSetDescription_GivenAggregateIsSome()
        {
            var createdEvent = this.fixture.Create<CreatedItemEvent>();
            var aggregate = new Item(this.fixture.Create<Guid>(), this.fixture.Create<string>());
            aggregate.Apply(createdEvent);
            aggregate.Description.IsSome.Should().Be(true);
            aggregate.Description.IfNone(string.Empty).Should().Be(createdEvent.Description.IfNone(string.Empty));
        }

        [Fact]
        public void ApplyUpdatedItemEvent_ShouldSetDescription_GivenAggregateIsSome()
        {
            var updatedEvent = this.fixture.Create<UpdatedItemEvent>();
            var aggregate = new Item(this.fixture.Create<Guid>(), this.fixture.Create<string>());
            aggregate.Apply(updatedEvent);
            aggregate.Description.IfNone(string.Empty).Should().Be(updatedEvent.NewDescription.IfNone(string.Empty));
        }

        [Fact]
        public void ApplyDeletedItemEvent_ShouldSetIsDeleted_GivenAggregateIsSome()
        {
            var deletedEvent = this.fixture.Create<DeletedItemEvent>();
            var aggregate = new Item(this.fixture.Create<Guid>(), this.fixture.Create<string>());
            aggregate.Apply(deletedEvent);
            aggregate.IsDeleted.Should().Be(true);
        }
    }
}