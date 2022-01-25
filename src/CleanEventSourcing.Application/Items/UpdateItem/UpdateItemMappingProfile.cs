using AutoMapper;

namespace CleanEventSourcing.Application.Items.UpdateItem
{
    public class UpdateItemMappingProfile : Profile
    {
        public UpdateItemMappingProfile()
        {
            this.CreateMap<Tuple<UpdateItemRouteRequest, UpdateItemBodyRequest>,
                    UpdateItemCommand>()
                .ForMember(command => command.Id,
                    configuration => configuration.MapFrom(source => source.Item1.Id))
                .ForMember(command => command.Description,
                    configuration => configuration.MapFrom(source => source.Item2.Description));
        }
    }
}