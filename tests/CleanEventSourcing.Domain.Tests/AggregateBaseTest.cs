using System.Linq;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace CleanEventSourcing.Domain.Tests
{
    public class AggregateBaseTest
    {
        private readonly Fixture fixture;

        public AggregateBaseTest()
        {
            this.fixture = new Fixture();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void AddEvent_ShouldAddEvent()
        {
            var integrationEvent = this.fixture.Create<DummyEvent>();
            var aggregateBase = new AggregateBase();
            aggregateBase.AddEvent(integrationEvent);
            aggregateBase.GetEvents().First().Should().Be(integrationEvent);
        }
    }
}