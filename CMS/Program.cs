    using Application.Interfaces;
    using Application.Services;
    using Domain.IRepositories.SqlServer;
    using Infrastructure.DependencyInjection;
    using Microsoft.AspNetCore.Authentication.Cookies;


    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddSession();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllersWithViews()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.IgnoreNullValues = true;
        });

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";  
        options.LogoutPath = "/Account/Logout";  
        options.AccessDeniedPath = "/Account/ErrorPage";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);  
        options.SlidingExpiration = true;  
    });






    #region Đăng ký Service

    builder.Services.AddScoped<INguoiDungService, NguoiDungService>();
    builder.Services.AddScoped<IVaiTroService, VaiTroService>();
    builder.Services.AddScoped<IMenuQuanTriService, MenuQuanTriService>();
    builder.Services.AddScoped<IMenuNguoiDungService, MenuNguoiDungService>();
    builder.Services.AddScoped<IQuyenSuDungService, QuyenSuDungService>();




    #endregion







    // Add services to the container.
    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.UseAuthentication();
    app.UseSession();

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=NguoiDung}/{action=Index}/{id?}");

    app.Run();
