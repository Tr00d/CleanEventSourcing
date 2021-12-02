using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private readonly Fixture fixture;
        private readonly MapperConfiguration configuration;
        
        public CreateItemMappingProfileTest()
        {
            fixture = new Fixture();
            configuration = new MapperConfiguration(configurationExpression => configurationExpression.AddProfile(new CreateItemMappingProfile()));
        }

        [Fact]
        public void MapperConfiguration_ShouldBeValid()
        {
            configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [MemberData(nameof(GetMappingTypes))]
        public void Map_ShouldConvertObject_GivenMappingIsRegistered(TypeMap type)
        {
            object sourceValue = new SpecimenContext(this.fixture).Resolve(type.SourceType);
            IMapper mapper = configuration.CreateMapper();
            object destinationValue = mapper.Map(sourceValue, type.SourceType, type.DestinationType);
            destinationValue.Should().BeEquivalentTo(sourceValue);
        }

        public static IEnumerable<object[]> GetMappingTypes => new MapperConfiguration(configurationExpression =>
            configurationExpression.AddProfile(new CreateItemMappingProfile())).GetAllTypeMaps().Select(typeMap => new object[] { typeMap });
    }
}