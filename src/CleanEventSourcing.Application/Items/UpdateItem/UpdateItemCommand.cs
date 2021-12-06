using System;
using MediatR;

namespace CleanEventSourcing.Application.Items.UpdateItem
{
    public class UpdateItemCommand : IRequest
    {
        public UpdateItemCommand()
        {
            this.Description = string.Empty;
        }

        public Guid Id { get; set; }

        public string Description { get; set; }
    }
}