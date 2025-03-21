﻿using Domain.IRepositories.SqlServer;
using Domain.Querys.Base;
using Infrastructure.Persitence.SqlServer;
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


            // Đăng ký tất cả các dịch vụ của repo
            services.AddScoped<INguoiDungRepository, NguoiDungRepository>();
            services.AddScoped<IVaiTroRepository, VaiTroRepository>();
            services.AddScoped<IMenuQuanTriRepository, MenuQuanTriRepository>();
            services.AddScoped<IMenuNguoiDungRepository, MenuNguoiDungRepository>();
            services.AddScoped<IQuyenSuDungRepository, QuyenSuDungRepository>();
            services.AddScoped<ResponeActionResult>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            return services;
        }
    }
}
