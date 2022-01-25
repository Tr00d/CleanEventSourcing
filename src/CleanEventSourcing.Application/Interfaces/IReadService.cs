using CleanEventSourcing.Domain.Items;
using LanguageExt;

namespace CleanEventSourcing.Application.Interfaces
{
    public interface IReadService
    {
        Task<Option<ItemSummary>> GetItemAsync(Guid id);
    }
}