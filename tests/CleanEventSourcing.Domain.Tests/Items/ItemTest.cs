using System;
using System.Linq;
using AutoFixture;
using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Domain.Items.Events;
using FluentAssertions;
using LanguageExt;
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
            Guid id = this.fixture.Create<Guid>();
            Item item = new Item(id, this.fixture.Create<string>());
            item.Id.Should().Be(id);
        }

        [Fact]
        public void Constructor_ShouldSetDescription()
        {
            string description = this.fixture.Create<string>();
            Item item = new Item(this.fixture.Create<Guid>(), description);
            item.Description.IsSome.Should().Be(true);
            item.Description.IfNone(string.Empty).Should().Be(description);
        }

        [Fact]
        public void GetIntegrationEvents_ShouldContainCreatedItemEvent_GivenItemIsCreated()
        {
            Guid id = this.fixture.Create<Guid>();
            string description = this.fixture.Create<string>();
            Item item = new Item(id, description);
            IIntegrationEvent[] events = item.GetIntegrationEvents().IfNone(Enumerable.Empty<IIntegrationEvent>())
                .ToArray();
            events.First().Should().BeOfType<CreatedItemEvent>();
            CreatedItemEvent result = (CreatedItemEvent) events.First();
            result.Id.Should().Be(id);
            result.Description.IsSome.Should().Be(true);
            result.Description.IfNone(string.Empty).Should().Be(description);
        }

        [Fact]
        public void GetIntegrationEvents_ShouldContainUpdatedItemEvent_GivenItemIsUpdated()
        {
            string description = this.fixture.Create<string>();
            Item item = new Item();
            item.Update(description);
            IIntegrationEvent[] events = item.GetIntegrationEvents().IfNone(Enumerable.Empty<IIntegrationEvent>())
                .ToArray();
            events.First().Should().BeOfType<UpdatedItemEvent>();
            UpdatedItemEvent result = (UpdatedItemEvent) events.First();
            result.OldDescription.IsNone.Should().Be(true);
            result.NewDescription.IfNone(string.Empty).Should().Be(description);
        }

        [Fact]
        public void Update_ShouldUpdateDescription()
        {
            string description = this.fixture.Create<string>();
            Item item = new Item();
            item.Update(description);
            item.Description.IfNone(string.Empty).Should().Be(description);
        }
    }
}