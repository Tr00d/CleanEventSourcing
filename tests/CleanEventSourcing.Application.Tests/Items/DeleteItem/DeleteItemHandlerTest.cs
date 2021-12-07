using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Application.Items.DeleteItem;
using CleanEventSourcing.Domain;
using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Domain.Items.Events;
using FluentAssertions;
using Moq;
using Xunit;

namespace CleanEventSourcing.Application.Tests.Items.DeleteItem
{
    public class DeleteItemHandlerTest
    {
        private readonly Fixture fixture;
        private readonly Mock<IRepository<Item>> mockRepository;

        public DeleteItemHandlerTest()
        {
            this.fixture = new Fixture();
            this.mockRepository = new Mock<IRepository<Item>>();
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_GivenRepositoryIsNull()
        {
            Action instantiation = () => new DeleteItemHandler(null);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("repository");
        }

        [Fact]
        public async Task Handle_ShouldDeleteItem()
        {
            Item aggregate = this.fixture.Create<Item>();
            DeleteItemCommand command = this.fixture.Create<DeleteItemCommand>();
            this.mockRepository.Setup(repository => repository.GetAsync(command.Id)).ReturnsAsync(aggregate);
            DeleteItemHandler handler = new DeleteItemHandler(this.mockRepository.Object);
            await handler.Handle(command, CancellationToken.None);
            aggregate.GetIntegrationEvents().IsSome.Should().Be(true);
            aggregate.GetIntegrationEvents().IfNone(Enumerable.Empty<IIntegrationEvent>()).First().Should()
                .BeOfType<DeletedItemEvent>();
        }

        [Fact]
        public async Task Handle_ShouldSaveItem()
        {
            Item aggregate = this.fixture.Create<Item>();
            DeleteItemCommand command = this.fixture.Create<DeleteItemCommand>();
            this.mockRepository.Setup(repository => repository.GetAsync(command.Id)).ReturnsAsync(aggregate);
            DeleteItemHandler handler = new DeleteItemHandler(this.mockRepository.Object);
            await handler.Handle(command, CancellationToken.None);
            this.mockRepository.Verify(repository => repository.SaveAsync(aggregate), Times.Once);
        }
    }
}