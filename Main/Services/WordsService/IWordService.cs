using CSharpFunctionalExtensions;
using TestWebApp.Services.WordsService.Models;

namespace TestWebApp.Services.WordsService;

public interface IWordService
{
    public Task<IEnumerable<WordDto>> GetWordsAsync(WordEnum type);
    
    public Task<IEnumerable<WordDto>> GetAllWords();
    
    public Task<IEnumerable<WordDto>> GetWordsAsync(IEnumerable<WordEnum> types);
    
    public Task<Result> AddWordsAsync(IEnumerable<WordDto> dtos);

    public Task<Result> AddWordAsync(WordDto dto);

    public Task<Result> UpdateWordsAsync(IEnumerable<WordDto> dtos);
    
    public Task<Result> DeleteWordsAsync(IEnumerable<WordDto> words);
}