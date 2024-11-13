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
            .AddDistributedMemoryCache()
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
        var connectionSettings = config.GetSection("ConnectionSettings");
        var connectionString = $"""
                                    Server={connectionSettings["Server"]};
                                    Database={connectionSettings["Database"]};
                                    User Id={connectionSettings["User Id"]};
                                    Password={connectionSettings["Password"]};
                                    TrustServerCertificate={connectionSettings["TrustServerCertificate"]};
                                    MultipleActiveResultSets={connectionSettings["MultipleActiveResultSets"]};
                                    Connection Timeout={connectionSettings["Connection Timeout"]};                                    
                               """;
        var commandTimeout = connectionSettings.GetValue<int>("Command Timeout");
        
        return services
            .AddDbContext<GermanLearningDbContext>(options => options
                .UseSqlServer(connectionString, sqlOptions => {
                    sqlOptions.CommandTimeout(commandTimeout);
                })
                .UseTriggers(triggerOptions => triggerOptions
                    .AddTrigger<CreateGradeTrigger>()
                )
            );
    }
    
    
    private static IServiceCollection AddCustomServices(this IServiceCollection services, IWebHostEnvironment env)
    {
        return services.AddServices(env);
    }
    
    public static IServiceCollection ConfigureAppServices(this IServiceCollection services, ConfigurationManager config, IWebHostEnvironment env)
    {
        return services
            .AddDatabaseServices(config)
            .AddSessionServices()
            .AddCustomServices(env);
    }
}