using System;
using AutoFixture;
using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Domain.Items.Events;
using FluentAssertions;
using LanguageExt;
using Xunit;

namespace CleanEventSourcing.Domain.Tests.Items.Events
{
    public class DeletedItemEventTest
    {
        private readonly Fixture fixture;

        public DeletedItemEventTest()
        {
            this.fixture = new Fixture();
        }

        [Fact]
        public void Constructor_ShouldSetId()
        {
            Guid id = this.fixture.Create<Guid>();
            DeletedItemEvent createdEvent =
                new DeletedItemEvent(id);
            createdEvent.Id.Should().Be(id);
        }

        [Fact]
        public void CanConvertTo_ShouldReturnTrue_GivenTypeIsItem()
        {
            DeletedItemEvent deletedEvent = this.fixture.Create<DeletedItemEvent>();
            bool result = deletedEvent.CanConvertTo<Item>();
            result.Should().Be(true);
        }

        [Fact]
        public void CanConvertTo_ShouldReturnFalse_GivenTypeIsNotItem()
        {
            DeletedItemEvent deletedEvent = this.fixture.Create<DeletedItemEvent>();
            bool result = deletedEvent.CanConvertTo<DummyAggregate>();
            result.Should().Be(false);
        }

        [Fact]
        public void ConvertTo_ShouldReturnNone_GivenTypeIsNotItem()
        {
            DeletedItemEvent deletedEvent = this.fixture.Create<DeletedItemEvent>();
            Option<IIntegrationEvent<DummyAggregate>> result = deletedEvent.ConvertTo<DummyAggregate>();
            result.IsNone.Should().Be(true);
        }

        [Fact]
        public void ConvertTo_ShouldReturnSome_GivenTypeIsItem()
        {
            DeletedItemEvent deletedEvent = this.fixture.Create<DeletedItemEvent>();
            Option<IIntegrationEvent<Item>> result = deletedEvent.ConvertTo<Item>();
            result.IsSome.Should().Be(true);
        }

        [Fact]
        public void Apply_ShouldSetIsDeleted_GivenAggregateIsSome()
        {
            Item aggregate = new Item();
            DeletedItemEvent deletedEvent = this.fixture.Create<DeletedItemEvent>();
            deletedEvent.Apply(aggregate);
            aggregate.IsDeleted.Should().Be(true);
        }
    }
}