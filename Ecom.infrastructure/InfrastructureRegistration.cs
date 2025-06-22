using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Repositories;
using Ecom.infrastructure.Repositories.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Ecom.infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection InfratructureConfiguration(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
            services.AddScoped<IUnitOfWOrk, UnitOfWork>();
            services.AddSingleton<IFileProvider>(
              new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
          );

            // ✅ يجب أن تكون Scoped وليس Singleton
            services.AddScoped<IImageManageService, ImageManageService>();

            // apply DBCOntext
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("EcomDatabase")); // Replace with your actual connection string
            });
            return services;
        }
    }
}
