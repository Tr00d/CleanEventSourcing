using System.Threading.Tasks;
using CleanEventSourcing.Application.Items.CreateItem;
using LanguageExt;

namespace CleanEventSourcing.Application.Items
{
    public class ItemService : IItemService
    {
        public Task CreateAsync(Option<CreateItemRequest> request)
        {
            throw new System.NotImplementedException();
        }
    }
}