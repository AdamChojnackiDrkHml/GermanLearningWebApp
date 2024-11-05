using CSharpFunctionalExtensions;
using TestWebApp.Data.Models.Grades;
using TestWebApp.Services.LearningControllerService.Models;
using TestWebApp.Services.LearningService;
using TestWebApp.Services.LearningService.Enums;
using TestWebApp.Services.LearningService.LearningServiceFactory;
using TestWebApp.Services.LearningService.Models;
using TestWebApp.Services.WordService.Enums;
using static TestWebApp.Services.WordService.Extensions.WordTypeExtensions;

namespace TestWebApp.Services.LearningControllerService.Implementation;

#nullable disable

public class LearningControllerService : ILearningControllerService
{
    private readonly ILearningServiceFactory _learningServiceFactory;

    private ILearningService _learningService;
    
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
    
    public Result<ExerciseDto> GetNextWord()
    {
        return _learningService.GetNextWord()
            .Map(wordToGrade => new ExerciseDto(
                WordToGrade: wordToGrade,
                QuestionWord: wordToGrade.Word.Translation
            )
        );
    }
    public ExerciseResultDto CheckAnswerAsync(ExerciseAnswerDto answer)
    {
        var result = _learningService.CheckAnswer(answer.Exercise.WordToGrade, answer.Answer);
        
        return new ExerciseResultDto(
            IsCorrect: result.IsCorrect,
            CorrectAnswer: result.CorrectAnswer
        );
    }

    public async Task SaveTrainingResultAsync()
    {
        await _learningService.SaveTrainingResultAsync();
    }
}