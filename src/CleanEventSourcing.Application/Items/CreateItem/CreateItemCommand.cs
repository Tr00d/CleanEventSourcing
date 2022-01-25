using MediatR;

namespace CleanEventSourcing.Application.Items.CreateItem
{
    public class CreateItemCommand : IRequest
    {
        public CreateItemCommand()
        {
            this.Description = string.Empty;
        }

        public Guid Id { get; set; }

        public string Description { get; set; }
    }
}