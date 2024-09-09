using CSharpFunctionalExtensions;
using TestWebApp.Data;
using TestWebApp.Services.LearningService.LearningCategories.Models;
using TestWebApp.Services.UserService;
using TestWebApp.Services.WordsService;

namespace TestWebApp.Services.LearningService.LearningCategories;

public abstract class LearningCategory
{
    protected readonly GermanLearningDbContext Context;
    protected readonly IWordService WordService;
    protected readonly IUserService UserService;

    protected List<GradeDto> GradedWords = [];
    private int _currentWordIndex = 0;

    protected LearningCategory(
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