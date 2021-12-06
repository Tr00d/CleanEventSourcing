using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Application.Items.UpdateItem;
using CleanEventSourcing.Domain.Items;
using FluentAssertions;
using Moq;
using Xunit;

namespace CleanEventSourcing.Application.Tests.Items.UpdateItem
{
    public class UpdateItemHandlerTest
    {
        private readonly Fixture fixture;
        private readonly Mock<IRepository<Item>> mockRepository;

        public UpdateItemHandlerTest()
        {
            this.fixture = new Fixture();
            this.mockRepository = new Mock<IRepository<Item>>();
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_GivenEventStoreIsNull()
        {
            Action instantiation = () => new UpdateItemHandler(null);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("repository");
        }

        [Fact]
        public async Task Handle_ShouldUpdateItem()
        {
            Item item = this.fixture.Create<Item>();
            UpdateItemCommand command = this.fixture.Create<UpdateItemCommand>();
            this.mockRepository.Setup(repository => repository.GetAsync(command.Id)).ReturnsAsync(item);
            UpdateItemHandler handler = new UpdateItemHandler(this.mockRepository.Object);
            await handler.Handle(command, CancellationToken.None);
            this.mockRepository.Verify(repository => repository.SaveAsync(item), Times.Once);
        }
    }
}