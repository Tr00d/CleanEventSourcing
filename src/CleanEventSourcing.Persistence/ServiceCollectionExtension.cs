using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Persistence.EventStore;
using CleanEventSourcing.Persistence.ReadModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanEventSourcing.Persistence
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Database"));
            });
            services.AddMediatR(typeof(ServiceCollectionExtension).Assembly);
            services.AddScoped<IReadService, InMemoryReadService>();
            services.AddScoped<IEventStore, InMemoryEventStore>();
        }
    }
}