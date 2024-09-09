using CSharpFunctionalExtensions;
using TestWebApp.Services.LearningService.Enums;
using TestWebApp.Services.LearningService.Models;

namespace TestWebApp.Services.LearningControllerService;

public interface ILearningControllerService
{
    public Task PrepareTrainingAsync(TrainingLevelEnum trainingLevel);

    public Result<GradeDto> GetNextWord();
    
    public Task SaveTrainingResultAsync();
    
    public Result SetLearningCategory(LearningCategoryEnum learningCategoryEnum);
}