using System.Threading.Tasks;
using CleanEventSourcing.Application.Items.CreateItem;
using LanguageExt;

namespace CleanEventSourcing.Application.Items
{
    public interface IItemService
    {
        Task CreateAsync(Option<CreateItemRequest> request);
    }
}