using AutoMapper;

namespace CleanEventSourcing.Application.Items.DeleteItem
{
    public class DeleteItemMappingProfile : Profile
    {
        public DeleteItemMappingProfile()
        {
            this.CreateMap<DeleteItemRequest, DeleteItemCommand>();
        }
    }
}