using CleanEventSourcing.Application.Interfaces;
using CleanEventSourcing.Domain.Items;
using CleanEventSourcing.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanEventSourcing.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Item>, Repository<Item>>();
        }
    }
}