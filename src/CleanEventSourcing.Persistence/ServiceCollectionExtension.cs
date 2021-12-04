using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Persistence.EventStore;
using CleanEventSourcing.Persistence.ReadModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanEventSourcing.Persistence
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterPersistence(this IServiceCollection services, string databaseName)
        {
            services.AddDbContext<Context>(options => options.UseInMemoryDatabase(databaseName));
            services.AddMediatR(typeof(ServiceCollectionExtension).Assembly);
            services.AddScoped<IReadService, InMemoryReadService>();
            services.AddScoped<IEventStore, InMemoryEventStore>();
        } 
    }
}