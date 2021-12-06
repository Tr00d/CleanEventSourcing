using System;
using System.Reflection;
using CleanEventSourcing.Application;
using CleanEventSourcing.Application.Items.CreateItem;
using CleanEventSourcing.Infrastructure;
using CleanEventSourcing.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CleanEventSourcing.Api
{
    public class Startup
    {
        private readonly string databaseName;

        public Startup()
        {
            this.databaseName = Guid.NewGuid().ToString();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);
            services.AddMvc()
                .AddFluentValidation(configuration =>
                    configuration.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                        .RegisterValidatorsFromAssemblyContaining<CreateItemRequestValidation>());
            services.RegisterApplication();
            services.RegisterPersistence(this.databaseName);
            services.RegisterInfrastructure();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}