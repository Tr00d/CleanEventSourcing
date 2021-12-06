using System;
using System.Collections.Generic;
using AutoFixture;
using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Domain.Items.Events;
using FluentAssertions;
using LanguageExt;
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
            string description = this.fixture.Create<string>();;
            UpdatedItemEvent updatedEvent =
                new UpdatedItemEvent(this.fixture.Create<Guid>(), description, this.fixture.Create<string>());
            updatedEvent.OldDescription.IfNone(string.Empty).Should().Be(description);
        }

        [Fact]
        public void Constructor_ShouldSetNewDescription()
        {
            string description = this.fixture.Create<string>();;
            UpdatedItemEvent updatedEvent =
                new UpdatedItemEvent(this.fixture.Create<Guid>(),this.fixture.Create<string>(), description);
            updatedEvent.NewDescription.IfNone(string.Empty).Should().Be(description);
        }
        
        [Fact]
        public void Constructor_ShouldSetId()
        {
            Guid id = this.fixture.Create<Guid>();
            UpdatedItemEvent updatedEvent =
                new UpdatedItemEvent(id,this.fixture.Create<string>(), this.fixture.Create<string>());
            updatedEvent.Id.Should().Be(id);
        }

        [Fact]
        public void CanConvertTo_ShouldReturnTrue_GivenTypeIsItem()
        {
            UpdatedItemEvent updatedEvent = this.fixture.Create<UpdatedItemEvent>();
            bool result = updatedEvent.CanConvertTo<Item>();
            result.Should().Be(true);
        }
        
        [Fact]
        public void CanConvertTo_ShouldReturnFalse_GivenTypeIsNotItem()
        {
            UpdatedItemEvent updatedEvent = this.fixture.Create<UpdatedItemEvent>();
            bool result = updatedEvent.CanConvertTo<DummyAggregate>();
            result.Should().Be(false);
        }

        [Fact]
        public void ConvertTo_ShouldReturnNone_GivenTypeIsNotItem()
        {
            UpdatedItemEvent updatedEvent = this.fixture.Create<UpdatedItemEvent>();
            Option<IIntegrationEvent<DummyAggregate>> result = updatedEvent.ConvertTo<DummyAggregate>();
            result.IsNone.Should().Be(true);
        }
        
        [Fact]
        public void ConvertTo_ShouldReturnSome_GivenTypeIsItem()
        {
            UpdatedItemEvent updatedEvent = this.fixture.Create<UpdatedItemEvent>();
            Option<IIntegrationEvent<Item>> result = updatedEvent.ConvertTo<Item>();
            result.IsSome.Should().Be(true);
        }

        [Fact]
        public void Apply_ShouldSetDescription_GivenAggregateIsSome()
        {
            Item aggregate = new Item();
            UpdatedItemEvent updatedEvent = this.fixture.Create<UpdatedItemEvent>();
            updatedEvent.Apply(aggregate);
            aggregate.Description.IsSome.Should().Be(true);
            aggregate.Description.IfNone(string.Empty).Should().Be(updatedEvent.NewDescription.IfNone(string.Empty));
        }

        private class DummyAggregate : IAggregate
        {
            public Guid Id { get; }
            public Option<IEnumerable<IIntegrationEvent>> GetIntegrationEvents() => throw new NotImplementedException();
        }
    }
}