using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Domain.Items.Events;
using Dawn;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanEventSourcing.Persistence.ReadModel
{
    public class InMemoryReadService : IReadService, INotificationHandler<CreatedItemEvent>,
        INotificationHandler<UpdatedItemEvent>
    {
        private readonly Context context;

        public InMemoryReadService(Context context)
        {
            this.context = Guard.Argument(context, nameof(context)).NotNull().Value;
        }

        public async Task Handle(CreatedItemEvent notification, CancellationToken cancellationToken)
        {
            await this.context.Items.AddAsync(
                new ItemSummary
                {
                    Id = notification.Id, Description = notification.Description.IfNone(string.Empty),
                }, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);
        }

        public async Task Handle(UpdatedItemEvent notification, CancellationToken cancellationToken)
        {
            ItemSummary item =
                await this.context.Items.FirstAsync(item => item.Id.Equals(notification.Id), cancellationToken);
            item.Description = notification.NewDescription.IfNone(string.Empty);
            await this.context.SaveChangesAsync(cancellationToken);
        }

        public Task<Option<ItemSummary>> GetItemAsync(Guid id) =>
            Task.FromResult(this.context.Items.FirstOrDefault(item => item.Id == id) ?? Option<ItemSummary>.None);
    }
}