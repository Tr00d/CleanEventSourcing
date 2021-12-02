using CleanEventSourcing.Application.Items;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanEventSourcing.Application
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceCollectionExtension).Assembly);
            services.AddMediatR(typeof(ServiceCollectionExtension).Assembly);
            services.AddScoped<IItemService, ItemService>();
        }
    }
}