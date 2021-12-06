using System.Threading;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Domain.Items;
using Dawn;
using MediatR;

namespace CleanEventSourcing.Application.Items.CreateItem
{
    public class CreateItemHandler : IRequestHandler<CreateItemCommand>
    {
        private readonly IRepository<Item> repository;

        public CreateItemHandler(IRepository<Item> repository)
        {
            this.repository = Guard.Argument(repository, nameof(repository)).NotNull().Value;
        }

        public async Task<Unit> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            Item item = new(request.Id, request.Description);
            await this.repository.SaveAsync(item);
            return Unit.Value;
        }
    }
}