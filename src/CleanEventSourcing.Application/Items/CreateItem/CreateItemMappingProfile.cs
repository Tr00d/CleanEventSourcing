using AutoMapper;

namespace CleanEventSourcing.Application.Items.CreateItem
{
    public class CreateItemMappingProfile : Profile
    {
        public CreateItemMappingProfile()
        {
            this.CreateMap<CreateItemRequest, CreateItemCommand>();
        }
    }
}