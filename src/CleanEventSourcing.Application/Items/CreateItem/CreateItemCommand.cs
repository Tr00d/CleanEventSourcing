using System;
using MediatR;

namespace CleanEventSourcing.Application.Items.CreateItem
{
    public class CreateItemCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Description { get; set; }
    }
}