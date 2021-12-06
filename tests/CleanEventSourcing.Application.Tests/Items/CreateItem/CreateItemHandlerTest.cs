using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Domain.Items;
using FluentAssertions;
using LanguageExt;
using Moq;
using Xunit;

namespace CleanEventSourcing.Application.Tests.Items.CreateItem
{
    public class CreateItemHandlerTest
    {
        private readonly Fixture fixture;
        private readonly Mock<IRepository<Item>> mockRepository;

        public CreateItemHandlerTest()
        {
            this.fixture = new Fixture();
            this.mockRepository = new Mock<IRepository<Item>>();
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_GivenRepositoryIsNull()
        {
            Action instantiation = () => new CreateItemHandler(null);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("repository");
        }

        [Fact]
        public async Task Handle_ShouldSaveItem()
        {
            CreateItemCommand command = this.fixture.Create<CreateItemCommand>();
            CreateItemHandler handler = new CreateItemHandler(this.mockRepository.Object);
            await handler.Handle(command, this.fixture.Create<CancellationToken>());
            this.mockRepository.Verify(
                repository => repository.SaveAsync(
                    It.Is<Option<Item>>(value =>
                        value.Match(item => item.Id, Guid.Empty).Equals(command.Id) && value
                            .Match(item => item.Description, string.Empty).Equals(command.Description))),
                Times.Once);
        }
    }
}