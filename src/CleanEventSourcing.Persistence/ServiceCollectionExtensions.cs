using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Persistence.EventStore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanEventSourcing.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterPersistence(this IServiceCollection services)
        {
            services.AddScoped<IEventStore, InMemoryEventStore>();
        }
    }
}