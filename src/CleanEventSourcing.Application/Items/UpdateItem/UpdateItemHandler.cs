using System.Threading;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Interfaces;
using Dawn;
using MediatR;

namespace CleanEventSourcing.Application.Items.UpdateItem
{
    public class UpdateItemHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IEventStore eventStore;
        public UpdateItemHandler(IEventStore eventStore)
        {
            this.eventStore = Guard.Argument(eventStore, nameof(eventStore)).NotNull().Value;
        }
        
        public Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken) => throw new System.NotImplementedException();
    }
}