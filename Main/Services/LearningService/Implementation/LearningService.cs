using CSharpFunctionalExtensions;
using TestWebApp.Services.LearningService.LearningCategories;
using TestWebApp.Services.LearningService.LearningCategories.CategoryFactory;
using TestWebApp.Services.LearningService.LearningCategories.Models;

namespace TestWebApp.Services.LearningService.Implementation;

#nullable disable

public class LearningService : ILearningService
{
    private readonly ILearningCategoryFactory _learningCategoryFactory;

    private LearningCategory _learningCategory;

    public LearningService(
        ILearningCategoryFactory learningCategoryFactory
    )
    {
        _learningCategoryFactory = learningCategoryFactory;
        SetLearningCategory(LearningCategoryEnum.Default);
    }
    
    public Result SetLearningCategory(LearningCategoryEnum learningCategoryEnum)
    {
        var learningCategoryResult = _learningCategoryFactory.CreateLearningCategory(learningCategoryEnum);
        
        if (learningCategoryResult.IsFailure)
        {
            return learningCategoryResult;
        }

        _learningCategory = learningCategoryResult.Value;
        return Result.Success();
    }
    
    public async Task PrepareTrainingAsync(TrainingLevelEnum trainingLevel)
    {
        await _learningCategory.PrepareTrainingAsync(trainingLevel);
    }
    
    public Result<GradeDto> GetNextWord()
    {
        return _learningCategory.GetNextWord();
    }
    
    public async Task SaveTrainingResultAsync()
    {
        await _learningCategory.SaveTrainingResultAsync();
    }
}