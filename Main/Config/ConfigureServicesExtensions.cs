using Microsoft.EntityFrameworkCore;
using TestWebApp.Data;
using TestWebApp.Data.Triggers;
using TestWebApp.Services;

namespace TestWebApp.Config;

public static class ConfigureServicesExtensions
{
    private static IServiceCollection AddSessionServices(this IServiceCollection services)
    {
        return services
            .AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            })
            .AddHttpContextAccessor();
    }
    
    private static IServiceCollection AddDatabaseServices(this IServiceCollection services, ConfigurationManager config)
    {
        return services
            .AddDbContext<GermanLearningDbContext>(options => options
                .UseSqlServer(config.GetConnectionString("DefaultConnection"))
                .UseTriggers(triggerOptions => triggerOptions
                    .AddTrigger<CreateGradeTrigger>()
                )
            );
    }
    
    
    private static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        return services.AddServices();
    }
    
    public static IServiceCollection ConfigureAppServices(this IServiceCollection services, ConfigurationManager config)
    {
        return services
            .AddDatabaseServices(config)
            .AddSessionServices()
            .AddCustomServices();
    }
}