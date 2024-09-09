using CSharpFunctionalExtensions;
using TestWebApp.Data;
using TestWebApp.Services.LearningService.Enums;
using TestWebApp.Services.LearningService.Models;
using TestWebApp.Services.UserService;
using TestWebApp.Services.WordService;

namespace TestWebApp.Services.LearningService.Implementation;

public abstract class LearningService : ILearningService
{
    protected readonly GermanLearningDbContext Context;
    protected readonly IWordService WordService;
    protected readonly IUserService UserService;

    protected List<GradeDto> GradedWords = [];
    private int _currentWordIndex = 0;

    protected LearningService(
        GermanLearningDbContext context,
        IWordService wordService,
        IUserService userService
    )
    {
        Context = context;
        WordService = wordService;
        UserService = userService;
    }

    public abstract Task PrepareTrainingAsync(TrainingLevelEnum trainingLevel);

    public Result<GradeDto> GetNextWord()
    {
        if (GradedWords.Count == 0)
        {
            return Result.Failure<GradeDto>("No words to learn");
        }

        if (_currentWordIndex >= GradedWords.Count)
        {
            return Result.Failure<GradeDto>("No more words to learn");
        }

        var word = Result.Success(GradedWords[_currentWordIndex]);
        _currentWordIndex++;

        return word;
    }

    public abstract Task SaveTrainingResultAsync();
    
}