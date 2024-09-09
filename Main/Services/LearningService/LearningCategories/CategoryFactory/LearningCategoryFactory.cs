using CSharpFunctionalExtensions;
using TestWebApp.Services.LearningService.LearningCategories.Categories;

namespace TestWebApp.Services.LearningService.LearningCategories.CategoryFactory;

public class LearningCategoryFactory : ILearningCategoryFactory
{
    private readonly IServiceProvider _serviceProvider;

    private readonly Dictionary<LearningCategoryEnum, LearningCategory> _categories;

    public LearningCategoryFactory(
        IServiceProvider serviceProvider
    )
    {
        _serviceProvider = serviceProvider;
        _categories = new Dictionary<LearningCategoryEnum, LearningCategory>
        {
            {LearningCategoryEnum.Default, _serviceProvider.GetRequiredService<DefaultLearningCategory>()}
        };
    }

    public Result<LearningCategory> CreateLearningCategory(LearningCategoryEnum learningCategoryEnum)
    {
        return _categories[learningCategoryEnum];
    }
}