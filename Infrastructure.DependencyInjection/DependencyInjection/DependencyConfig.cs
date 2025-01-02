using Domain.IRepositories.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Specialized;
using System.Configuration;
using Unity;
using Unity.Lifetime;

namespace Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            // Đăng ký tất cả các dịch vụ của Infrastructure hello
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
