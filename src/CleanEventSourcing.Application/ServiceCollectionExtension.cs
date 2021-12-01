using CleanEventSourcing.Application.Items;
using Microsoft.Extensions.DependencyInjection;

namespace CleanEventSourcing.Application
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterApplication(this IServiceCollection services)
        {
            //services.AddMediatR(typeof(CreateItemHandler).Assembly);
            services.AddScoped<IService, Service>();
        }
    }
}