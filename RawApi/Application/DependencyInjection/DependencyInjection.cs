using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RawApi.Application.Interfaces;
using RawApi.Domain.Entities;
using RawApi.Infra.DependencyService.Services.Auth;
using RawApi.Infra;

namespace RawApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
         IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")));

            //Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

            return services;
        }
    }
}
