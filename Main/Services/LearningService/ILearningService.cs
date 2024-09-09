using CSharpFunctionalExtensions;
using TestWebApp.Services.LearningService.Enums;
using TestWebApp.Services.LearningService.Models;

namespace TestWebApp.Services.LearningService;

public interface ILearningService
{
    Task PrepareTrainingAsync(TrainingLevelEnum trainingLevel);

    Result<GradeDto> GetNextWord();

    Task SaveTrainingResultAsync();
}