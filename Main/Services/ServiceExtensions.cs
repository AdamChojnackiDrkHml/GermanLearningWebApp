using Microsoft.AspNetCore.Identity;
using TestWebApp.Data.Models.Users;
using TestWebApp.Services.LearningControllerService;
using TestWebApp.Services.LearningService;
using TestWebApp.Services.LearningService.Implementation;
using TestWebApp.Services.LearningService.LearningServiceFactory;
using TestWebApp.Services.UserService;
using TestWebApp.Services.WordService;

namespace TestWebApp.Services;

public static class ServiceExtensions
{
    private static IServiceCollection AddLearningService(this IServiceCollection services)
    {
        return services
            .AddScoped<ILearningService, DefaultLearningService>()
            .AddScoped<DefaultLearningService>()
            .AddScoped<ILearningServiceFactory, LearningServiceFactory>();
    }
    
    private static IServiceCollection AddLearningControllerService(this IServiceCollection services)
    {
        return services
            .AddScoped<ILearningControllerService, LearningControllerService.Implementation.LearningControllerService>();
    }
    
    private static IServiceCollection AddWordService(this IServiceCollection services)
    {
        return services
            .AddScoped<IWordService, WordService.Implementation.WordService>();
    }
    
    private static IServiceCollection AddUserService(this IServiceCollection services, IWebHostEnvironment env)
    {
        services = services
            .AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        
        if (env.IsDevelopment())
        {
            services = services
                .AddScoped<IUserService, UserService.Implementation.DevelopmentUserService>();
        }
        else
        {
            services = services
                .AddScoped<IUserService, UserService.Implementation.UserService>();
        }

        return services;
    }
    
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        return services
            .AddExceptionHandler<ExceptionHandler.ExceptionHandler>();
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services, IWebHostEnvironment env)
    {
        return services
            .AddExceptionHandling()
            .AddLearningService()
            .AddLearningControllerService()
            .AddWordService()
            .AddUserService(env);
    }

}