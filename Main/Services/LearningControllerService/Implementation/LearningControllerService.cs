using CSharpFunctionalExtensions;
using TestWebApp.Services.LearningService.Enums;
using TestWebApp.Services.LearningService.LearningServiceFactory;
using TestWebApp.Services.LearningService.Models;

namespace TestWebApp.Services.LearningControllerService.Implementation;

#nullable disable

public class LearningControllerService : ILearningControllerService
{
    private readonly ILearningServiceFactory _learningServiceFactory;

    private LearningService.Implementation.LearningService _learningService;

    public LearningControllerService(
        ILearningServiceFactory learningServiceFactory
    )
    {
        _learningServiceFactory = learningServiceFactory;
        SetLearningCategory(LearningCategoryEnum.Default);
    }
    
    public Result SetLearningCategory(LearningCategoryEnum learningCategoryEnum)
    {
        var learningCategoryResult = _learningServiceFactory.CreateLearningCategory(learningCategoryEnum);
        
        if (learningCategoryResult.IsFailure)
        {
            return learningCategoryResult;
        }

        _learningService = learningCategoryResult.Value;
        return Result.Success();
    }
    
    public async Task PrepareTrainingAsync(TrainingLevelEnum trainingLevel)
    {
        await _learningService.PrepareTrainingAsync(trainingLevel);
    }
    
    public Result<GradeDto> GetNextWord()
    {
        return _learningService.GetNextWord();
    }
    
    public async Task SaveTrainingResultAsync()
    {
        await _learningService.SaveTrainingResultAsync();
    }
}