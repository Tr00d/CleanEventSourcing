using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Application.Items.UpdateItem;
using CleanEventSourcing.Domain;
using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Domain.Items.Events;
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
        [Trait("Category", "Unit")]
        public void Constructor_ShouldThrowArgumentNullException_GivenEventStoreIsNull()
        {
            Action instantiation = () => new UpdateItemHandler(null);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("repository");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Handle_ShouldUpdateItem()
        {
            var item = this.fixture.Create<Item>();
            var command = this.fixture.Create<UpdateItemCommand>();
            this.mockRepository.Setup(repository => repository.GetAsync(command.Id)).ReturnsAsync(item);
            var handler = new UpdateItemHandler(this.mockRepository.Object);
            await handler.Handle(command, CancellationToken.None);
            item.GetIntegrationEvents().IsSome.Should().Be(true);
            item.GetIntegrationEvents().IfNone(Enumerable.Empty<IIntegrationEvent>()).First().Should()
                .BeOfType<UpdatedItemEvent>();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Handle_ShouldSaveItem()
        {
            var item = this.fixture.Create<Item>();
            var command = this.fixture.Create<UpdateItemCommand>();
            this.mockRepository.Setup(repository => repository.GetAsync(command.Id)).ReturnsAsync(item);
            var handler = new UpdateItemHandler(this.mockRepository.Object);
            await handler.Handle(command, CancellationToken.None);
            this.mockRepository.Verify(repository => repository.SaveAsync(item), Times.Once);
        }
    }
}