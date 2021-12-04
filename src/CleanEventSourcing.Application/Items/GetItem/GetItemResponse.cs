using System;

namespace CleanEventSourcing.Application.Items.GetItem
{
    public class GetItemResponse
    {
        public Guid Id { get; set; }
        
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }
    }
}