using AutoMapper;
using CleanEventSourcing.Domain.Items;

namespace CleanEventSourcing.Application.Items.GetItem
{
    public class GetItemMappingProfile: Profile
    {
        public GetItemMappingProfile()
        {
            this.CreateMap<GetItemRequest, GetItemQuery>();
            this.CreateMap<ItemSummary, GetItemResponse>();
        }
    }
}