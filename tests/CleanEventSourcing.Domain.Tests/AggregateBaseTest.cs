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
        public void AddEvent_ShouldAddEvent()
        {
            DummyEvent integrationEvent = this.fixture.Create<DummyEvent>();
            AggregateBase aggregateBase = new AggregateBase();
            aggregateBase.AddEvent(integrationEvent);
            aggregateBase.GetEvents().First().Should().Be(integrationEvent);
        }
    }
}