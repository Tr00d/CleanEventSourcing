using System;
using Dawn;

namespace CleanEventSourcing.Application.Items.CreateItem
{
    public class CreateItemRequest
    {
        public string Description { get; set; }
        
        public Guid Id { get; }

        public CreateItemRequest()
        {
            Id = Guid.NewGuid();
        }
    }
}