using System.Threading;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Domain.Items;
using Dawn;
using LanguageExt;
using MediatR;
using Unit = MediatR.Unit;

namespace CleanEventSourcing.Application.Items.UpdateItem
{
    public class UpdateItemHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IRepository<Item> repository;

        public UpdateItemHandler(IRepository<Item> repository)
        {
            this.repository = Guard.Argument(repository, nameof(repository)).NotNull().Value;
        }

        public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            Option<Item> item = await this.repository.GetAsync(request.Id);
            item.IfSome(value => value.Update(request.Description));
            await this.repository.SaveAsync(item);
            return Unit.Value;
        }
    }
}