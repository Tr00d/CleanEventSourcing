using System;

namespace CleanEventSourcing.Domain.Items
{
    public class ItemSummary
    {
        public Guid Id { get; set; }
        
        public string Description { get; set; }
    }
}