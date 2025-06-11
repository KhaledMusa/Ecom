using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        public static IServiceCollection InfratructureConfiguration(this IServiceCollection services,IConfiguration configuration)
        {

            // services.AddScoped<IGenericRepositry<Category>, GenericRepositry<Category>>();
            // services.AddScoped<IGenericRepositry<Product>, GenericRepositry<Product>>();
            // services.AddScoped<IGenericRepositry<Photo>, GenericRepositry<Photo>>();

            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
            services.AddScoped<IUnitOfWOrk, UnitOfWork>();
            // apply DBCOntext
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("EcomDatabase")); // Replace with your actual connection string
            });
            return services;
        }
    }
}
