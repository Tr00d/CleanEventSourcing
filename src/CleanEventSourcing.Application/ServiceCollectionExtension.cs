using CleanEventSourcing.Application.Items;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanEventSourcing.Application
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterApplication(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddAutoMapper(typeof(ServiceCollectionExtension).Assembly);
            services.AddMediatR(typeof(ServiceCollectionExtension).Assembly);
            services.AddScoped<IItemService, ItemService>();
        }
    }
}