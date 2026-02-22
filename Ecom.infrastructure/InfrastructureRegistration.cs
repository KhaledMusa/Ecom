using Ecom.Core.Entities.Identity;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Repositories;
using Ecom.infrastructure.Repositories.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using StackExchange.Redis;
using System.IO;
using Ecom.infrastructure.Repositories;
using Ecom.infrastructure.Repositories.Service;
using Microsoft.AspNetCore.Identity;
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
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IUnitOfWOrk, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton<IFileProvider>(
              new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
          );

            // ✅ يجب أن تكون Scoped وليس Singleton
            services.AddSingleton<IImageManageService, ImageManageService>();

            // apply DBCOntext
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("EcomDatabase")); // Replace with your actual connection string
            });

            // Redis Configuration
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var options = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
            });

            // Identity Configurations
            services.AddIdentityCore<AppUser>(opt =>
            {
                // configure identity options here if needed later
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();

            return services;
        }
    }
}
