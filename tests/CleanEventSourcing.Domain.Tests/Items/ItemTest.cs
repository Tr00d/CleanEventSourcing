using System;
using System.Collections.Generic;
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
            fixture = new Fixture();
        }

        [Fact]
        public void Constructor_ShouldSetId()
        {
            Guid id = fixture.Create<Guid>();
            Item item = new Item(id, fixture.Create<string>());
            item.Id.Should().Be(id);
        }

        [Fact]
        public void Constructor_ShouldSetDescription()
        {
            string description = fixture.Create<string>();
            Item item = new Item(fixture.Create<Guid>(), description);
            item.Description.IsSome.Should().Be(true);
            item.Description.IfNone(string.Empty).Should().Be(description);
        }

        [Fact]
        public void GetIntegrationEvents_ShouldContainCreateItemEvent_GivenItemIsCreated()
        {
            Guid id = fixture.Create<Guid>();
            string description = fixture.Create<string>();
            Item item = new Item(id, description);
            IIntegrationEvent[] events = item.GetIntegrationEvents().IfNone(new List<IIntegrationEvent>()).ToArray();
            events.First().Should().BeOfType<CreatedItemEvent>();
            CreatedItemEvent result = (CreatedItemEvent) events.First();
            result.Id.Should().Be(id);
            result.Description.IsSome.Should().Be(true);
            result.Description.IfNone(string.Empty).Should().Be(description);
        }

        [Fact]
        public void GetStream_ShouldReturnClassNameWithId()
        {
            Guid id = fixture.Create<Guid>();
            string description = fixture.Create<string>();
            string expectedStream = $"{nameof(Item)}-{id}";
            Item item = new Item(id, description);
            item.GetStream().IsSome.Should().Be(true);
            item.GetStream().IfNone(string.Empty).Should().Be(expectedStream);
        }
    }
}