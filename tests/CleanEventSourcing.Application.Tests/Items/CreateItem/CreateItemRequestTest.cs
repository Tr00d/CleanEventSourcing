using AutoFixture;
using CleanEventSourcing.Application.Items.CreateItem;
using FluentAssertions;
using Xunit;

namespace CleanEventSourcing.Application.Tests.Items.CreateItem
{
    public class CreateItemRequestTest
    {
        private readonly Fixture fixture;

        public CreateItemRequestTest()
        {
            this.fixture = new Fixture();
        }

        [Fact]
        public void Constructor_ShouldInstantiateRequestWithNonEmptyGuid()
        {
            CreateItemRequest request = new CreateItemRequest();
            request.Id.Should().NotBeEmpty();
        }
    }
}