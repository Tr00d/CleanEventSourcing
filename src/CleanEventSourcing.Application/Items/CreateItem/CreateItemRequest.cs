using System;

namespace CleanEventSourcing.Application.Items.CreateItem
{
    public class CreateItemRequest
    {
        public CreateItemRequest()
        {
            this.Id = Guid.NewGuid();
        }

        public string Description { get; set; }

        public Guid Id { get; }
    }
}