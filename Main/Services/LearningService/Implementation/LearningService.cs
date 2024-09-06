using Microsoft.EntityFrameworkCore;
using TestWebApp.Data;
using TestWebApp.Data.Models.Genders;
using TestWebApp.Data.Models.Words;
using TestWebApp.Services.WordsService;

namespace TestWebApp.Services.LearningService.Implementation;

public class LearningService : ILearningService
{
    private readonly GermanLearningDbContext _context;
    private readonly IWordService _wordService;

    public LearningService(GermanLearningDbContext context, IWordService wordService)
    {
        _context = context;
        _wordService = wordService;
    }

    public async Task<IEnumerable<Word>> GetTrainingWordsAsync(
        WordEnum type, 
        TrainingLevelEnum trainingLevel,
        int userId,
        int count
    )
    {
        var userGrades = _context.Grades
            .Where(grade => grade.UserId == userId)
            .OrderBy(grade => grade.Value);
        
        var leveledGrades = trainingLevel switch
        {
            TrainingLevelEnum.Easy => userGrades.Take(count),
            TrainingLevelEnum.Hard => userGrades.Skip(userGrades.Count() - count),
            _ => throw new ArgumentOutOfRangeException(nameof(trainingLevel), trainingLevel, null)
        };

        
        return await leveledGrades
            .Select(grade => grade.Word)
            .OfType(type)
            .ToListAsync();
    }

    public async Task SaveTrainingResultAsync(IEnumerable<Word> words)
    {
        var baseType = typeof(Word);
        var wordsList = words.ToList();

        var derivedTypes = baseType.Assembly.GetTypes()
            .Where(t => t.IsSubclassOf(baseType) && !t.IsAbstract);

        foreach (var type in derivedTypes)
        {
            var method = _wordService.GetType().GetMethod("UpdateWordsAsync")!.MakeGenericMethod(type);

            var filteredList = wordsList.Where(item => item.GetType() == type);

            await (Task)method.Invoke(this, [filteredList])!;
        }
    }

    public async Task<IEnumerable<Gender>> GetGendersAsync()
    {
        return await _context.Genders.ToListAsync();
    }
}