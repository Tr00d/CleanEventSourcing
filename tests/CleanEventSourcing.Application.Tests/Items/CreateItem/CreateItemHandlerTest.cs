using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Domain;
using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Domain.Items.Events;
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
        [Trait("Category", "Unit")]
        public void Constructor_ShouldThrowArgumentNullException_GivenRepositoryIsNull()
        {
            Action instantiation = () => new CreateItemHandler(null);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("repository");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Handle_ShouldSaveItem()
        {
            var command = this.fixture.Create<CreateItemCommand>();
            var handler = new CreateItemHandler(this.mockRepository.Object);
            await handler.Handle(command, CancellationToken.None);
            this.mockRepository.Verify(
                repository => repository.SaveAsync(
                    It.Is<Option<Item>>(value =>
                        value.Match(item => item.Id, Guid.Empty).Equals(command.Id) && value
                            .Match(item => item.Description, string.Empty).Equals(command.Description))),
                Times.Once);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Handle_ShouldCreateItem()
        {
            var command = this.fixture.Create<CreateItemCommand>();
            var handler = new CreateItemHandler(this.mockRepository.Object);
            await handler.Handle(command, CancellationToken.None);
            var aggregate = this.mockRepository.Invocations.First().Arguments.FirstOrDefault() is Option<Item>
                ? (Option<Item>)this.mockRepository.Invocations.First().Arguments.FirstOrDefault()
                : Option<Item>.None;
            aggregate.IsSome.Should().Be(true);
            aggregate.IfNone(() => throw new InvalidOperationException()).GetIntegrationEvents().IsSome.Should()
                .Be(true);
            aggregate.IfNone(() => throw new InvalidOperationException()).GetIntegrationEvents()
                .IfNone(Enumerable.Empty<IIntegrationEvent>()).First().Should()
                .BeOfType<CreatedItemEvent>();
        }
    }
}