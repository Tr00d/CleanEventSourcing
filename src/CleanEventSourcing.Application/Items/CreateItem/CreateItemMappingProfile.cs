using AutoMapper;

namespace CleanEventSourcing.Application.Items.CreateItem
{
    public class CreateItemMappingProfile : Profile
    {
        public CreateItemMappingProfile()
        {
            CreateMap<CreateItemRequest, CreateItemCommand>();
        }
    }
}