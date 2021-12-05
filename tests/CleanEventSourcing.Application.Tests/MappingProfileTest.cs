using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Kernel;
using AutoMapper;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Application.Items.GetItem;
using FluentAssertions;
using Xunit;

namespace CleanEventSourcing.Application.Tests
{
    public class MappingProfileTest
    {
        private readonly MapperConfiguration configuration;
        private readonly Fixture fixture;

        public MappingProfileTest()
        {
            this.fixture = new Fixture();
            this.configuration = GetConfiguration();
        }

        public static IEnumerable<object[]> GetMappingTypes => GetConfiguration().GetAllTypeMaps()
            .Select(typeMap => new object[] {typeMap});

        private static MapperConfiguration GetConfiguration() => new MapperConfiguration(configurationExpression =>
        {
            configurationExpression.AddProfile<CreateItemMappingProfile>();
            configurationExpression.AddProfile<GetItemMappingProfile>();
        });

        [Fact]
        public void MapperConfiguration_ShouldBeValid() => this.configuration.AssertConfigurationIsValid();

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