using System;
using System.Threading;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Domain;
using CleanEventSourcing.Domain.Items;
using LanguageExt;
using MediatR;

namespace CleanEventSourcing.Persistence.ReadModel
{
    public class InMemoryReadService : IReadService, INotificationHandler<IIntegrationEvent>
    {
        public Task Handle(IIntegrationEvent notification, CancellationToken cancellationToken) => throw new System.NotImplementedException();
        
        public Task<Option<ItemSummary>> GetItemAsync(Guid id) => throw new NotImplementedException();
    }
}