using CSharpFunctionalExtensions;
using TestWebApp.Services.LearningService.Enums;
using TestWebApp.Services.LearningService.Implementation;

namespace TestWebApp.Services.LearningService.LearningServiceFactory;

public class LearningServiceFactory : ILearningServiceFactory
{
    private readonly Dictionary<LearningCategoryEnum, ILearningService> _categories;

    public LearningServiceFactory(
        IServiceProvider serviceProvider
    )
    {
        _categories = new Dictionary<LearningCategoryEnum, ILearningService>
        {
            {LearningCategoryEnum.Default, serviceProvider.GetRequiredService<DefaultLearningService>()}
        };
    }

    public Result<ILearningService> CreateLearningCategory(LearningCategoryEnum learningCategoryEnum)
    {
        return !_categories.TryGetValue(learningCategoryEnum, out var category) 
            ? Result.Failure<ILearningService>($"Learning category {learningCategoryEnum} is not supported") 
            : Result.Success(category);

    }
}