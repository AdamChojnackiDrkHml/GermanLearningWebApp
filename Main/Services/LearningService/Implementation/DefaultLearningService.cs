using Microsoft.EntityFrameworkCore;
using TestWebApp.Data;
using TestWebApp.Services.LearningService.Enums;
using TestWebApp.Services.UserService;
using TestWebApp.Services.WordService;
using static TestWebApp.Services.LearningService.Mappers.GradeMapper;

namespace TestWebApp.Services.LearningService.Implementation;

public class DefaultLearningService : LearningService
{
    private const int Count = 10;
    
    public DefaultLearningService(GermanLearningDbContext context, IWordService wordService,
        IUserService userService
    ) : base(context, wordService, userService)
    {
    }

    public override async Task PrepareTrainingAsync(TrainingLevelEnum trainingLevel)
    {
        var user = UserService.GetSessionUserAsync();
        
        var userGrades = Context.Grades
            .Where(grade => grade.UserId == user.Value.UserId)
            .OrderBy(grade => grade.Value);

        var leveledGrades = trainingLevel switch
        {
            TrainingLevelEnum.Easy => userGrades.Take(Count),
            TrainingLevelEnum.Hard => userGrades.Skip(userGrades.Count() - Count),
            _ => throw new ArgumentOutOfRangeException(nameof(trainingLevel), trainingLevel, null)
        };

        
        GradedWords = await leveledGrades
            .Select(grade => grade.ToDto())
            .ToListAsync();
    }

    public override async Task SaveTrainingResultAsync()
    {
        GradedWords.ToList().ForEach(grade => Context.Grades.Update(grade.ToEntity(Context)));
        
        await Context.SaveChangesAsync();
    }
}