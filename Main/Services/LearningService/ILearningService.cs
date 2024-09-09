using CSharpFunctionalExtensions;
using TestWebApp.Services.LearningService.LearningCategories;
using TestWebApp.Services.LearningService.LearningCategories.Models;

namespace TestWebApp.Services.LearningService;

public interface ILearningService
{
    public Task PrepareTrainingAsync(TrainingLevelEnum trainingLevel);

    public Result<GradeDto> GetNextWord();
    
    public Task SaveTrainingResultAsync();
    
    public Result SetLearningCategory(LearningCategoryEnum learningCategoryEnum);
}