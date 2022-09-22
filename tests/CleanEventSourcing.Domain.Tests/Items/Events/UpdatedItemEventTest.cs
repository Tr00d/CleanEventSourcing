using System;
using AutoFixture;
using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Domain.Items.Events;
using FluentAssertions;
using Xunit;

namespace CleanEventSourcing.Domain.Tests.Items.Events
{
    public class UpdatedItemEventTest
    {
        private readonly Fixture fixture;

        public UpdatedItemEventTest()
        {
            this.fixture = new Fixture();
        }

        [Fact]
        public void Constructor_ShouldSetOldDescription()
        {
            var description = this.fixture.Create<string>();
            ;
            var updatedEvent =
                new UpdatedItemEvent(this.fixture.Create<Guid>(), description, this.fixture.Create<string>());
            updatedEvent.OldDescription.IfNone(string.Empty).Should().Be(description);
        }

        [Fact]
        public void Constructor_ShouldSetNewDescription()
        {
            var description = this.fixture.Create<string>();
            ;
            var updatedEvent =
                new UpdatedItemEvent(this.fixture.Create<Guid>(), this.fixture.Create<string>(), description);
            updatedEvent.NewDescription.IfNone(string.Empty).Should().Be(description);
        }

        [Fact]
        public void Constructor_ShouldSetId()
        {
            var id = this.fixture.Create<Guid>();
            var updatedEvent =
                new UpdatedItemEvent(id, this.fixture.Create<string>(), this.fixture.Create<string>());
            updatedEvent.Id.Should().Be(id);
        }

        [Fact]
        public void CanConvertTo_ShouldReturnTrue_GivenTypeIsItem()
        {
            var updatedEvent = this.fixture.Create<UpdatedItemEvent>();
            var result = updatedEvent.CanConvertTo<Item>();
            result.Should().Be(true);
        }

        [Fact]
        public void CanConvertTo_ShouldReturnFalse_GivenTypeIsNotItem()
        {
            var updatedEvent = this.fixture.Create<UpdatedItemEvent>();
            var result = updatedEvent.CanConvertTo<DummyAggregate>();
            result.Should().Be(false);
        }

        [Fact]
        public void ConvertTo_ShouldReturnNone_GivenTypeIsNotItem()
        {
            var updatedEvent = this.fixture.Create<UpdatedItemEvent>();
            var result = updatedEvent.ConvertTo<DummyAggregate>();
            result.IsNone.Should().Be(true);
        }

        [Fact]
        public void ConvertTo_ShouldReturnSome_GivenTypeIsItem()
        {
            var updatedEvent = this.fixture.Create<UpdatedItemEvent>();
            var result = updatedEvent.ConvertTo<Item>();
            result.IsSome.Should().Be(true);
        }

        [Fact]
        public void Apply_ShouldSetDescription_GivenAggregateIsSome()
        {
            var aggregate = new Item();
            var updatedEvent = this.fixture.Create<UpdatedItemEvent>();
            updatedEvent.Apply(aggregate);
            aggregate.Description.IsSome.Should().Be(true);
            aggregate.Description.IfNone(string.Empty).Should().Be(updatedEvent.NewDescription.IfNone(string.Empty));
        }
    }
}