using System;
using CleanEventSourcing.Api.Items;
using FluentAssertions;
using Xunit;

namespace CleanEventSourcing.Api.Tests.Items
{
    public class ItemsControllerTest
    {
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_GivenServiceIsNull()
        {
            Action instantiation = () => new ItemsController(null);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("service");
        }
    }
}