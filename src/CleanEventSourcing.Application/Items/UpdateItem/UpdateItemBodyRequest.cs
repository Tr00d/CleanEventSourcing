namespace CleanEventSourcing.Application.Items.UpdateItem
{
    public class UpdateItemBodyRequest
    {
        public UpdateItemBodyRequest()
        {
            this.Description = string.Empty;
        }

        public string Description { get; set; }
    }
}