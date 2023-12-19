using DataBase;
using DataBase.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Services;
using Services.Interfaces;
using Services.Options;

namespace API.Extenstions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        return services
                .AddTransient<IEmailSender, EmailSender>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IInstallationService, InstallationService>()
                .AddScoped<IPanelService, PanelService>()
                .AddScoped<IInstallationReportService, InstallationReportService>()
                .AddScoped<IPanelReportService, PanelReportService>()
                .AddSingleton<IIoTService, IoTService>()
            ;
    }

    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddAuthentication()
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
        return services;
    }

    public static IServiceCollection AddServicesOptions(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<AdminOptions>(
                configuration.GetSection(AdminOptions.Section))
            .Configure<PaginationOptions>(
                configuration.GetSection(PaginationOptions.Section))
            .Configure<EmailSenderOptions>(
                configuration.GetSection(EmailSenderOptions.Section));
    }
}