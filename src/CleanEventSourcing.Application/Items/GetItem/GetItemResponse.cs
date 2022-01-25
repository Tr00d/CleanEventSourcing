namespace CleanEventSourcing.Application.Items.GetItem
{
    public class GetItemResponse
    {
        public GetItemResponse()
        {
            this.Description = string.Empty;
        }

        public Guid Id { get; set; }

        public string Description { get; set; }
    }
}