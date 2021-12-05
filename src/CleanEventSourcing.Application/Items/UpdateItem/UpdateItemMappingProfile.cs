using AutoMapper;

namespace CleanEventSourcing.Application.Items.UpdateItem
{
    public class UpdateItemMappingProfile : Profile
    {
        public UpdateItemMappingProfile()
        {
            this.CreateMap<(UpdateItemRouteRequest routeRequest, UpdateItemBodyRequest bodyRequest),
                    UpdateItemCommand>()
                .ForMember(command => command.Id,
                    configuration => configuration.MapFrom(source => source.routeRequest.Id))
                .ForMember(command => command.Description,
                    configuration => configuration.MapFrom(source => source.bodyRequest.Description));
        }
    }
}