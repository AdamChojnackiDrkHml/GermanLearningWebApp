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
    
    private static IServiceCollection AddUserService(this IServiceCollection services)
    {
        return services
            .AddScoped<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddScoped<IUserService, UserService.Implementation.UserService>();
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddLearningService()
            .AddLearningControllerService()
            .AddWordService()
            .AddUserService();
    }

}