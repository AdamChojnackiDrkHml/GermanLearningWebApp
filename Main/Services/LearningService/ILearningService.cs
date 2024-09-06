using TestWebApp.Data.Models.Genders;
using TestWebApp.Data.Models.Words;
using TestWebApp.Services.WordsService;

namespace TestWebApp.Services.LearningService;

public interface ILearningService
{
    public Task<IEnumerable<Word>> GetTrainingWordsAsync(WordEnum type, TrainingLevelEnum trainingLevel, int userId, int count);
    
    public Task SaveTrainingResultAsync(IEnumerable<Word> words);
    
    public Task<IEnumerable<Gender>> GetGendersAsync();
}