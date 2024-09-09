using EntityFrameworkCore.Triggered;
using TestWebApp.Data.Models.Grades;
using TestWebApp.Data.Models.Users;
using TestWebApp.Data.Models.Words;

namespace TestWebApp.Data.Triggers;

public class CreateGradeTrigger : IAfterSaveAsyncTrigger<Word>
{
    private readonly GermanLearningDbContext _context;

    public CreateGradeTrigger(GermanLearningDbContext context)
    {
        _context = context;
    }

    public async Task AfterSaveAsync(ITriggerContext<Word> triggerContext, CancellationToken cancellationToken)
    {
        var word = triggerContext.Entity;

        var grades = _context.Users
            .Select(user => CreateGrade(word, user));
        
        await _context.Grades.AddRangeAsync(grades, cancellationToken);
    }
    
    private static Grade CreateGrade(Word word, User user)
    {
        return new Grade
        {
            Word = word,
            UserId = user.Id,
            Value = 0,
        };
    }
}