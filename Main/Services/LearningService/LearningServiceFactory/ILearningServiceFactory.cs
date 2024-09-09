using CSharpFunctionalExtensions;
using TestWebApp.Services.LearningService.Enums;

namespace TestWebApp.Services.LearningService.LearningServiceFactory;

public interface ILearningServiceFactory
{
    public Result<Implementation.LearningService> CreateLearningCategory(LearningCategoryEnum learningCategoryEnum);
}