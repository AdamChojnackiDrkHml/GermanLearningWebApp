using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using TestWebApp.Data;
using TestWebApp.Services.WordsService.Mappers;
using TestWebApp.Services.WordsService.Models;

namespace TestWebApp.Services.WordsService.Implementation;

public class WordService(GermanLearningDbContext context) : IWordService
{
    public async Task<IEnumerable<WordDto>> GetWordsAsync(WordEnum type)
    {
        var x =  await context
            .Words
            .OfType(type)
            .ToListAsync();
        
        return x.Select(WordMapper.ToDto);
    }
    
    public async Task<IEnumerable<WordDto>> GetAllWords()
    {
        var words = new List<WordDto>();
        var types = Enum.GetValues(typeof(WordEnum));
        foreach (var type in Enum.GetValues(typeof(WordEnum)))
        {
            words.AddRange(await GetWordsAsync((WordEnum)type));
        }

        return words;
    }
    
    public async Task<IEnumerable<WordDto>> GetWordsAsync(IEnumerable<WordEnum> types)
    {
        return (
            await Task.WhenAll(types.Select(async x => await GetWordsAsync(x))
            )
        ).SelectMany(x => x);
    }

    public async Task<Result> AddWordsAsync(IEnumerable<WordDto> dtos)
    {
        await context.Words
            .AddRangeAsync(dtos.Select(dto => dto.ToEntity(context)));
        return await SaveChangesAsync();
    }

    public async Task<Result> AddWordAsync(WordDto dto)
    {
        await context.Words.AddAsync(dto.ToEntity(context));
        return await SaveChangesAsync();
    }
    
    public async Task<Result> UpdateWordsAsync(IEnumerable<WordDto> dtos)
    {
        var modifiedWordsList = dtos
            .OrderBy(dto => dto.Id)
            .ToList();

        var existingTWords = await context
            .Words
            .Where(w => modifiedWordsList.Select(dto => dto.Id).Contains(w.Id))
            .OrderBy(w => w.Id)
            .ToListAsync();

        for(var i = 0; i < modifiedWordsList.Count; i++)
        {
            context
                .Entry(existingTWords[i]).CurrentValues
                .SetValues(modifiedWordsList[i]);
        }
        
        context.UpdateRange(existingTWords);
        
        return await SaveChangesAsync();
    }

    public async Task<Result> DeleteWordsAsync(IEnumerable<WordDto> words)
    {
        var modifiedWordsList = words.ToList();

        var tWordsToDelete = await context
            .Words
            .Where(v => modifiedWordsList.Select(w => w.Id).Contains(v.Id))
            .ToListAsync();
        
        context.RemoveRange(tWordsToDelete);
        
        return await SaveChangesAsync();
    }
    
    private async Task<Result> SaveChangesAsync()
    {
        try
        {
            await context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure(e.Message);
        }
    }
    
}