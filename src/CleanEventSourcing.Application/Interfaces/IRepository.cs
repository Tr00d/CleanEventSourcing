using System;
using System.Threading.Tasks;
using CleanEventSourcing.Domain;
using LanguageExt;

namespace CleanEventSourcing.Application.Interfaces
{
    public interface IRepository<T> where T : IAggregate, new()
    {
        Task SaveAsync(Option<T> aggregate);

        Task<Option<T>> GetAsync(Guid id);
    }
}