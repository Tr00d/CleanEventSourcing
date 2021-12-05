using System;
using MediatR;

namespace CleanEventSourcing.Application.Items.UpdateItem
{
    public class UpdateItemCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Description { get; set; }
    }
}