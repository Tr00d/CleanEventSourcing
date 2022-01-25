using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Application.Items.DeleteItem;
using CleanEventSourcing.Application.Items.GetItem;
using CleanEventSourcing.Application.Items.UpdateItem;
using LanguageExt;

namespace CleanEventSourcing.Application.Items
{
    public interface IItemService
    {
        Task CreateAsync(Option<CreateItemRequest> request);

        Task<Option<GetItemResponse>> GetAsync(Option<GetItemRequest> request);

        Task UpdateAsync(Option<UpdateItemRouteRequest> routeRequest, Option<UpdateItemBodyRequest> bodyRequest);

        Task DeleteAsync(Option<DeleteItemRequest> request);
    }
}