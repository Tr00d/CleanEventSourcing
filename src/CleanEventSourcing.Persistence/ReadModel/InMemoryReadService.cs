using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Domain;
using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Domain.Items.Events;
using Dawn;
using LanguageExt;
using MediatR;

namespace CleanEventSourcing.Persistence.ReadModel
{
    public class InMemoryReadService : IReadService, INotificationHandler<CreatedItemEvent>
    {
        private readonly Context context;

        public InMemoryReadService(Context context)
        {
            this.context = Guard.Argument(context, nameof(context)).NotNull().Value;
        }

        public Task<Option<ItemSummary>> GetItemAsync(Guid id) =>
           Task.FromResult(this.context.Items.FirstOrDefault(item => item.Id == id) ?? Option<ItemSummary>.None);

        public async Task Handle(CreatedItemEvent notification, CancellationToken cancellationToken)
        {
            await this.context.Items.AddAsync(new ItemSummary() {Id = notification.Id, Description = notification.Description.IfNone(string.Empty), CreationDate = notification.CreationDate}, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);
        }
    }
}