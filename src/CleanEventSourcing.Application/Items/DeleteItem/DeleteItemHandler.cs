using System.Threading;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Domain.Items;
using Dawn;
using LanguageExt;
using MediatR;
using Unit = MediatR.Unit;

namespace CleanEventSourcing.Application.Items.DeleteItem
{
    public class DeleteItemHandler : IRequestHandler<DeleteItemCommand>
    {
        private readonly IRepository<Item> repository;

        public DeleteItemHandler(IRepository<Item> repository)
        {
            this.repository = Guard.Argument(repository, nameof(repository)).NotNull().Value;
        }

        public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            Option<Item> item = await this.repository.GetAsync(request.Id);
            item.IfSome(value => value.Delete());
            await this.repository.SaveAsync(item);
            return Unit.Value;
        }
    }
}