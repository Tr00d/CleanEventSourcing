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
            fixture = new Fixture();
        }

        [Fact]
        public void Constructor_ShouldSetId()
        {
            Guid id = fixture.Create<Guid>();
            CreatedItemEvent createdEvent = new CreatedItemEvent(id, fixture.Create<string>());
            createdEvent.Id.Should().Be(id);
        }

        [Fact]
        public void Constructor_ShouldSetDescription()
        {
            string description = fixture.Create<string>();
            CreatedItemEvent createdEvent = new CreatedItemEvent(fixture.Create<Guid>(), description);
            createdEvent.Description.Should().Be(description);
        }
    }
}