using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Application.Items.GetItem;
using CleanEventSourcing.Domain.Items;
using FluentAssertions;
using LanguageExt;
using Moq;
using Xunit;

namespace CleanEventSourcing.Application.Tests.Items.GetItem
{
    public class GetItemHandlerTest
    {
        private readonly Fixture fixture;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IReadService> mockReadService;

        public GetItemHandlerTest()
        {
            this.fixture = new Fixture();
            this.mockReadService = new Mock<IReadService>();
            this.mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_GivenReadServiceIsNull()
        {
            Action instantiation = () => new GetItemHandler(null);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("readService");
        }

        [Fact]
        public async Task Handle_ShouldReturnValueFromService()
        {
            GetItemQuery query = this.fixture.Create<GetItemQuery>();
            ItemSummary item = this.fixture.Create<ItemSummary>();
            this.mockReadService.Setup(service => service.GetItemAsync(query.Id)).ReturnsAsync(item);
            GetItemHandler handler = new GetItemHandler(this.mockReadService.Object);
            Option<ItemSummary> result = await handler.Handle(query, CancellationToken.None);
        }
    }
}