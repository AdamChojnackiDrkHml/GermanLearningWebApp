using CSharpFunctionalExtensions;
using TestWebApp.Services.LearningService.Enums;

namespace TestWebApp.Services.LearningService.LearningServiceFactory;

public interface ILearningServiceFactory
{
    public Result<ILearningService> CreateLearningCategory(LearningCategoryEnum learningCategoryEnum);
}