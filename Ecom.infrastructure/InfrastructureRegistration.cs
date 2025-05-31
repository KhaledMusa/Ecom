using Ecom.Core.Interfaces;
using Ecom.infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection InfratructureConfiguration(this IServiceCollection services)
        {
            // Register your DbContext, Repositories, and other infrastructure services here
            // Example: services.AddDbContext<AppDbContext>(options => options.UseSqlServer("YourConnectionString"));
            
            // services.AddScoped<IGenericRepositry<Category>, GenericRepositry<Category>>();
            // services.AddScoped<IGenericRepositry<Product>, GenericRepositry<Product>>();
            // services.AddScoped<IGenericRepositry<Photo>, GenericRepositry<Photo>>();
            services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }
}
