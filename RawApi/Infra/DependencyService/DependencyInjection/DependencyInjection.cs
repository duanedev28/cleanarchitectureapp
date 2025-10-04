using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RawApi.Domain;
using RawApi.Domain.Entities;


namespace RawApi.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            //Sql connection
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")));

            //Repository
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //Services
            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
            return services;
        }
    }
}
