using TestWebApp.Data.Models.Words;

namespace TestWebApp.Services.WordsService;

public static class WordTypeExtensions
{
    public static IQueryable<Word> OfType(this IQueryable<Word> words, WordEnum type)
    {
        return type switch
        {
            WordEnum.Verb => words.OfType<Verb>(),
            WordEnum.Noun => words.OfType<Noun>(),
            WordEnum.Adjective => words.OfType<Adjective>(),
            WordEnum.Adverb => words.OfType<Adverb>(),
            WordEnum.Misc => words.OfType<Misc>(),
            _ => words,
        };
    }
    
}