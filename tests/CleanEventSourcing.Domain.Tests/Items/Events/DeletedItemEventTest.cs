using System;
using AutoFixture;
using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Domain.Items.Events;
using CleanEventSourcing.Tests.Shared;
using FluentAssertions;
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
        [Trait("Category", "Unit")]
        public void Constructor_ShouldSetId()
        {
            var id = this.fixture.Create<Guid>();
            var createdEvent =
                new DeletedItemEvent(id);
            createdEvent.Id.Should().Be(id);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CanConvertTo_ShouldReturnTrue_GivenTypeIsItem()
        {
            var deletedEvent = this.fixture.Create<DeletedItemEvent>();
            var result = deletedEvent.CanConvertTo<Item>();
            result.Should().Be(true);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CanConvertTo_ShouldReturnFalse_GivenTypeIsNotItem()
        {
            var deletedEvent = this.fixture.Create<DeletedItemEvent>();
            var result = deletedEvent.CanConvertTo<DummyAggregate>();
            result.Should().Be(false);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ConvertTo_ShouldReturnNone_GivenTypeIsNotItem()
        {
            var deletedEvent = this.fixture.Create<DeletedItemEvent>();
            var result = deletedEvent.ConvertTo<DummyAggregate>();
            result.IsNone.Should().Be(true);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ConvertTo_ShouldReturnSome_GivenTypeIsItem()
        {
            var deletedEvent = this.fixture.Create<DeletedItemEvent>();
            var result = deletedEvent.ConvertTo<Item>();
            result.IsSome.Should().Be(true);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Apply_ShouldSetIsDeleted_GivenAggregateIsSome()
        {
            var aggregate = new Item();
            var deletedEvent = this.fixture.Create<DeletedItemEvent>();
            deletedEvent.Apply(aggregate);
            aggregate.IsDeleted.Should().Be(true);
        }
    }
}