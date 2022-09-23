using CleanEventSourcing.Application.Items.CreateItem;
using FluentAssertions;
using Xunit;

namespace CleanEventSourcing.Application.Tests.Items.CreateItem
{
    public class CreateItemRequestTest
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void Constructor_ShouldInstantiateRequestWithNonEmptyGuid() =>
            new CreateItemRequest().Id.Should().NotBeEmpty();
    }
}