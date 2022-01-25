namespace CleanEventSourcing.Domain.Items
{
    public class ItemSummary
    {
        public ItemSummary()
        {
            this.Description = string.Empty;
        }

        public Guid Id { get; set; }

        public string Description { get; set; }
    }
}