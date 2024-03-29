using AutoFixture;
using AutoMapper;
using CleanEventSourcing.Application.Items.UpdateItem;
using FluentAssertions;
using Xunit;
using static LanguageExt.Prelude;

namespace CleanEventSourcing.Application.Tests.Items.UpdateItem
{
    public class UpdateItemMappingProfileTest
    {
        private readonly Fixture fixture;

        public UpdateItemMappingProfileTest()
        {
            this.fixture = new Fixture();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Map_ShouldConvertSourceToDestination()
        {
            var routeRequest = this.fixture.Create<UpdateItemRouteRequest>();
            var bodyRequest = this.fixture.Create<UpdateItemBodyRequest>();
            var configuration = new MapperConfiguration(configurationExpression =>
                configurationExpression.AddProfile<UpdateItemMappingProfile>());
            var mapper = configuration.CreateMapper();
            var command = mapper.Map<UpdateItemCommand>(Tuple(routeRequest, bodyRequest));
            command.Id.Should().Be(routeRequest.Id);
            command.Description.Should().Be(bodyRequest.Description);
        }
    }
}