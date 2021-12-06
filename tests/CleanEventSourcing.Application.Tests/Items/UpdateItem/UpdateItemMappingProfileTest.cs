using AutoFixture;
using AutoMapper;
using CleanEventSourcing.Application.Items.UpdateItem;
using FluentAssertions;
using static LanguageExt.Prelude;
using Xunit;

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
        public void Map_ShouldConvertSourceToDestination()
        {
            UpdateItemRouteRequest routeRequest = this.fixture.Create<UpdateItemRouteRequest>();
            UpdateItemBodyRequest bodyRequest = this.fixture.Create<UpdateItemBodyRequest>();
            MapperConfiguration configuration = new MapperConfiguration(configurationExpression => configurationExpression.AddProfile<UpdateItemMappingProfile>());
            IMapper mapper = configuration.CreateMapper();
            UpdateItemCommand command = mapper.Map<UpdateItemCommand>(Tuple(routeRequest, bodyRequest));
            command.Id.Should().Be(routeRequest.Id);
            command.Description.Should().Be(bodyRequest.Description);
        }
    }
}