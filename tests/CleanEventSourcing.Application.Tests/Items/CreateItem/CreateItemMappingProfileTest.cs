using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Kernel;
using AutoMapper;
using CleanEventSourcing.Application.Items.CreateItem;
using FluentAssertions;
using Xunit;

namespace CleanEventSourcing.Application.Tests.Items.CreateItem
{
    public class CreateItemMappingProfileTest
    {
        private readonly MapperConfiguration configuration;
        private readonly Fixture fixture;

        public CreateItemMappingProfileTest()
        {
            this.fixture = new Fixture();
            this.configuration = new MapperConfiguration(configurationExpression =>
                configurationExpression.AddProfile(new CreateItemMappingProfile()));
        }

        public static IEnumerable<object[]> GetMappingTypes => new MapperConfiguration(configurationExpression =>
                configurationExpression.AddProfile(new CreateItemMappingProfile())).GetAllTypeMaps()
            .Select(typeMap => new object[] {typeMap});

        [Fact]
        public void MapperConfiguration_ShouldBeValid()
        {
            this.configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [MemberData(nameof(GetMappingTypes))]
        public void Map_ShouldConvertObject_GivenMappingIsRegistered(TypeMap type)
        {
            object sourceValue = new SpecimenContext(this.fixture).Resolve(type.SourceType);
            IMapper mapper = this.configuration.CreateMapper();
            object destinationValue = mapper.Map(sourceValue, type.SourceType, type.DestinationType);
            destinationValue.Should().BeEquivalentTo(sourceValue);
        }
    }
}