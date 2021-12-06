using System;
using AutoFixture;
using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Domain.Items.Events;
using FluentAssertions;
using LanguageExt;
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
        public void Constructor_ShouldSetId()
        {
            Guid id = this.fixture.Create<Guid>();
            CreatedItemEvent createdEvent =
                new CreatedItemEvent(id, this.fixture.Create<string>());
            createdEvent.Id.Should().Be(id);
        }

        [Fact]
        public void Constructor_ShouldSetDescription()
        {
            string description = this.fixture.Create<string>();
            CreatedItemEvent createdEvent =
                new CreatedItemEvent(this.fixture.Create<Guid>(), description);
            createdEvent.Description.IsSome.Should().Be(true);
            createdEvent.Description.IfNone(string.Empty).Should().Be(description);
        }

        [Fact]
        public void CanConvertTo_ShouldReturnTrue_GivenTypeIsItem()
        {
            CreatedItemEvent createdEvent = this.fixture.Create<CreatedItemEvent>();
            bool result = createdEvent.CanConvertTo<Item>();
            result.Should().Be(true);
        }

        [Fact]
        public void CanConvertTo_ShouldReturnFalse_GivenTypeIsNotItem()
        {
            CreatedItemEvent createdEvent = this.fixture.Create<CreatedItemEvent>();
            bool result = createdEvent.CanConvertTo<DummyAggregate>();
            result.Should().Be(false);
        }

        [Fact]
        public void ConvertTo_ShouldReturnNone_GivenTypeIsNotItem()
        {
            CreatedItemEvent createdEvent = this.fixture.Create<CreatedItemEvent>();
            Option<IIntegrationEvent<DummyAggregate>> result = createdEvent.ConvertTo<DummyAggregate>();
            result.IsNone.Should().Be(true);
        }

        [Fact]
        public void ConvertTo_ShouldReturnSome_GivenTypeIsItem()
        {
            CreatedItemEvent createdEvent = this.fixture.Create<CreatedItemEvent>();
            Option<IIntegrationEvent<Item>> result = createdEvent.ConvertTo<Item>();
            result.IsSome.Should().Be(true);
        }

        [Fact]
        public void Apply_ShouldSetId_GivenAggregateIsSome()
        {
            Item aggregate = new Item();
            CreatedItemEvent createdEvent = this.fixture.Create<CreatedItemEvent>();
            createdEvent.Apply(aggregate);
            aggregate.Id.Should().Be(createdEvent.Id);
        }

        [Fact]
        public void Apply_ShouldSetDescription_GivenAggregateIsSome()
        {
            Item aggregate = new Item();
            CreatedItemEvent createdEvent = this.fixture.Create<CreatedItemEvent>();
            createdEvent.Apply(aggregate);
            aggregate.Description.IsSome.Should().Be(true);
            aggregate.Description.IfNone(string.Empty).Should().Be(createdEvent.Description.IfNone(string.Empty));
        }
    }
}