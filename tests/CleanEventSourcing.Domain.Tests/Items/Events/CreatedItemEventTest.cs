using System;
using AutoFixture;
using CleanEventSourcing.Domain.Items.Events;
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
    }
}