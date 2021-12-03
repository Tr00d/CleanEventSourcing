using System.Threading.Tasks;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Application.Items.GetItem;
using LanguageExt;

namespace CleanEventSourcing.Application.Items
{
    public interface IItemService
    {
        Task CreateAsync(Option<CreateItemRequest> request);

        Task<Option<GetItemResponse>> GetAsync(Option<GetItemRequest> request);
    }
}