using System.Threading;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Domain;
using MediatR;

namespace CleanEventSourcing.Persistence.ReadModel
{
    public class InMemoryReadModel : IReadModel, INotificationHandler<IIntegrationEvent>
    {
        public Task Handle(IIntegrationEvent notification, CancellationToken cancellationToken) => throw new System.NotImplementedException();
    }
}