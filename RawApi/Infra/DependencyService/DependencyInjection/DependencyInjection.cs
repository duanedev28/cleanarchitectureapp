using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RawApi.Application;
using RawApi.Application.Interfaces;
using RawApi.Domain;
using RawApi.Domain.Entities;
using RawApi.Infra.DependencyService.Services.Auth;

namespace RawApi.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")));

            //Repository
            services.AddScoped<IProductRepository, ProductRepository>();

            //Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
