using CSharpFunctionalExtensions;

namespace TestWebApp.Services.LearningService.LearningCategories.CategoryFactory;

public interface ILearningCategoryFactory
{
    public Result<LearningCategory> CreateLearningCategory(LearningCategoryEnum learningCategoryEnum);
}