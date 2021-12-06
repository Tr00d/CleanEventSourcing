using System;

namespace CleanEventSourcing.Application.Items.CreateItem
{
    public class CreateItemRequest
    {
        public CreateItemRequest()
        {
            this.Id = Guid.NewGuid();
            this.Description = string.Empty;
        }

        public string Description { get; set; }

        public Guid Id { get; }
    }
}