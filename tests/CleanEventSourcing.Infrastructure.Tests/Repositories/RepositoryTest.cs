using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Domain;
using CleanEventSourcing.Infrastructure.Repositories;
using FluentAssertions;
using LanguageExt;
using MediatR;
using static LanguageExt.Prelude;
using Moq;
using Xunit;

namespace CleanEventSourcing.Infrastructure.Tests.Repositories
{
    public class RepositoryTest
    {
        private readonly Fixture fixture;
        private readonly Mock<IEventStore> mockEventStore;
        private readonly Mock<IMediator> mockMediator;
        
        public RepositoryTest()
        {
            this.fixture = new Fixture();
            this.mockEventStore = new Mock<IEventStore>();
            this.mockMediator = new Mock<IMediator>();
        }
        
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_GivenEventStoreIsNull()
        {
            Action instantiation = () => new Repository<DummyAggregate>(null, this.mockMediator.Object);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("eventStore");
        }
        
        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_GivenMediatorIsNull()
        {
            Action instantiation = () => new Repository<DummyAggregate>(this.mockEventStore.Object, null);
            instantiation.Should().ThrowExactly<ArgumentNullException>().WithParameterName("mediator");
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNone_GivenEventStoreReturnsNoEvents()
        {
            Guid id = this.fixture.Create<Guid>();
            this.mockEventStore.Setup(store => store.GetEvents($"{nameof(DummyAggregate)}-{id}"))
                .ReturnsAsync(Option<IEnumerable<IIntegrationEvent>>.None);
            Repository<DummyAggregate> repository = new Repository<DummyAggregate>(this.mockEventStore.Object, this.mockMediator.Object);
            Option<DummyAggregate> result = await repository.GetAsync(id);
            result.IsNone.Should().Be(true);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnSome_GivenEventStoreReturnsEvents()
        {
            Guid id = this.fixture.Create<Guid>();
            IEnumerable<IIntegrationEvent> events =
                this.fixture.CreateMany<DummyEvent>();    
            this.mockEventStore.Setup(store => store.GetEvents($"{nameof(DummyAggregate)}-{id}"))
                .ReturnsAsync(Some(events));
            Repository<DummyAggregate> repository = new Repository<DummyAggregate>(this.mockEventStore.Object, this.mockMediator.Object);
            Option<DummyAggregate> result = await repository.GetAsync(id);
            result.IsSome.Should().Be(true);
        }
        
        [Fact]
        public async Task GetAsync_ShouldReturnUpToDateAggregate_GivenEventStoreReturnsEvents()
        {
            Guid id = this.fixture.Create<Guid>();
            IEnumerable<IIntegrationEvent> integrationEvents = this.fixture.CreateMany<DummyEvent>().ToArray();
            this.mockEventStore.Setup(store => store.GetEvents($"{nameof(DummyAggregate)}-{id}"))
                .ReturnsAsync(Some(integrationEvents));
            Repository<DummyAggregate> repository = new Repository<DummyAggregate>(this.mockEventStore.Object, this.mockMediator.Object);
            Option<DummyAggregate> result = await repository.GetAsync(id);
            DummyAggregate aggregate = result.IfNone(() => throw new InvalidOperationException());
            aggregate.EventCount.Should().Be(integrationEvents.Count());
        }

        [Fact]
        public async Task SaveAsync_ShouldPublishEvents_GivenAggregateIsSome()
        {
            DummyAggregate aggregate = this.fixture.Create<DummyAggregate>();
            Repository<DummyAggregate> repository = new Repository<DummyAggregate>(this.mockEventStore.Object, this.mockMediator.Object);
            await repository.SaveAsync(aggregate);
        }
        
        [Fact]
        public async Task SaveAsync_ShouldNotPublishAnyEvent_GivenAggregateIsNone()
        {
            Repository<DummyAggregate> repository = new Repository<DummyAggregate>(this.mockEventStore.Object, this.mockMediator.Object);
            await repository.SaveAsync(Option<DummyAggregate>.None);
            this.mockMediator.Verify(mediator => mediator.Publish(It.IsAny<DummyEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }
        
        private class DummyAggregate : IAggregate
        {
            public Guid Id { get; set; }
            public Option<string> GetStream() => throw new NotImplementedException();

            public Option<IEnumerable<IIntegrationEvent>> GetIntegrationEvents() =>
                new List<DummyEvent>() {new DummyEvent()};
            public int EventCount { get; set; }
        }

        private class DummyEvent : IIntegrationEvent, IIntegrationEvent<DummyAggregate>
        {
            public void Apply(Option<DummyAggregate> aggregate) => aggregate.IfSome(value => value.EventCount++);
            public Option<string> Stream { get; }
            public DateTime CreationDate { get; }
            public Type GetEventType() => typeof(DummyEvent);
            public bool CanConvertTo<T>() where T : IAggregate => typeof(T) == typeof(DummyAggregate);
            public Option<IIntegrationEvent<T>> ConvertTo<T>() where T : IAggregate => this.CanConvertTo<T>() ? Some((IIntegrationEvent<T>)this) : Option<IIntegrationEvent<T>>.None;
        }
    }
}