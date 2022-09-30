using CleanEventSourcing.Application.Items;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Application.Items.DeleteItem;
using CleanEventSourcing.Application.Items.GetItem;
using CleanEventSourcing.Application.Items.UpdateItem;
using FluentValidation;
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
            services.AddScoped<IValidator, CreateItemRequestValidation>();
            services.AddScoped<IValidator, DeleteItemRequestValidation>();
            services.AddScoped<IValidator, GetItemRequestValidation>();
            services.AddScoped<IValidator, UpdateItemRouteRequestValidation>();
            services.AddScoped<IValidator, UpdateItemBodyRequestValidation>();
        }
    }
}