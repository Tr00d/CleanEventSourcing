using System;
using AutoFixture;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Application.Items.UpdateItem;
using FluentAssertions;
using Moq;
using Xunit;

namespace CleanEventSourcing.Application.Tests.Items.UpdateItem
{
    public class UpdateItemHandlerTest
    {
        private readonly Mock<IEventStore> mockEventStore;
        
        public UpdateItemHandlerTest()
        {
            this.mockEventStore = new Mock<IEventStore>();
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_GivenEventStoreIsNull()
        {
            Action instantiation = () => new UpdateItemHandler(null);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("eventStore");
        }
    }
}