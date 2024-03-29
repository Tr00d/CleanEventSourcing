using System;
using AutoFixture;
using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Domain.Items.Events;
using CleanEventSourcing.Tests.Shared;
using FluentAssertions;
using Xunit;

namespace CleanEventSourcing.Domain.Tests.Items.Events
{
    public class CreatedItemEventTest
    {
        private readonly Fixture fixture;

        public CreatedItemEventTest()
        {
            this.fixture = new Fixture();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Constructor_ShouldSetId()
        {
            var id = this.fixture.Create<Guid>();
            var createdEvent =
                new CreatedItemEvent(id, this.fixture.Create<string>());
            createdEvent.Id.Should().Be(id);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Constructor_ShouldSetDescription()
        {
            var description = this.fixture.Create<string>();
            var createdEvent =
                new CreatedItemEvent(this.fixture.Create<Guid>(), description);
            createdEvent.Description.IsSome.Should().Be(true);
            createdEvent.Description.IfNone(string.Empty).Should().Be(description);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CanConvertTo_ShouldReturnTrue_GivenTypeIsItem()
        {
            var createdEvent = this.fixture.Create<CreatedItemEvent>();
            var result = createdEvent.CanConvertTo<Item>();
            result.Should().Be(true);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CanConvertTo_ShouldReturnFalse_GivenTypeIsNotItem()
        {
            var createdEvent = this.fixture.Create<CreatedItemEvent>();
            var result = createdEvent.CanConvertTo<DummyAggregate>();
            result.Should().Be(false);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ConvertTo_ShouldReturnNone_GivenTypeIsNotItem()
        {
            var createdEvent = this.fixture.Create<CreatedItemEvent>();
            var result = createdEvent.ConvertTo<DummyAggregate>();
            result.IsNone.Should().Be(true);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ConvertTo_ShouldReturnSome_GivenTypeIsItem()
        {
            var createdEvent = this.fixture.Create<CreatedItemEvent>();
            var result = createdEvent.ConvertTo<Item>();
            result.IsSome.Should().Be(true);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Apply_ShouldSetId_GivenAggregateIsSome()
        {
            var aggregate = new Item();
            var createdEvent = this.fixture.Create<CreatedItemEvent>();
            createdEvent.Apply(aggregate);
            aggregate.Id.Should().Be(createdEvent.Id);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Apply_ShouldSetDescription_GivenAggregateIsSome()
        {
            var aggregate = new Item();
            var createdEvent = this.fixture.Create<CreatedItemEvent>();
            createdEvent.Apply(aggregate);
            aggregate.Description.IsSome.Should().Be(true);
            aggregate.Description.IfNone(string.Empty).Should().Be(createdEvent.Description.IfNone(string.Empty));
        }
    }
}