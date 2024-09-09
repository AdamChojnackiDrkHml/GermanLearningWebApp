using CSharpFunctionalExtensions;
using TestWebApp.Services.LearningService.Enums;
using TestWebApp.Services.LearningService.Implementation;

namespace TestWebApp.Services.LearningService.LearningServiceFactory;

public class LearningServiceFactory : ILearningServiceFactory
{
    private readonly Dictionary<LearningCategoryEnum, Implementation.LearningService> _categories;

    public LearningServiceFactory(
        IServiceProvider serviceProvider
    )
    {
        _categories = new Dictionary<LearningCategoryEnum, Implementation.LearningService>
        {
            {LearningCategoryEnum.Default, serviceProvider.GetRequiredService<DefaultLearningService>()}
        };
    }

    public Result<Implementation.LearningService> CreateLearningCategory(LearningCategoryEnum learningCategoryEnum)
    {
        return _categories[learningCategoryEnum];
    }
}