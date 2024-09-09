using TestWebApp.Data;
using TestWebApp.Data.Models.Words;
using TestWebApp.Services.WordService.Enums;
using TestWebApp.Services.WordService.Models;

namespace TestWebApp.Services.WordService.Mappers;

public static class WordMapper
{
    public static Word ToEntity(this WordDto wordDto, GermanLearningDbContext context)
    {
        return wordDto.Type switch
        {
            WordEnum.Noun => new Noun
            {
                Id = wordDto.Id,
                Spelling = wordDto.Spelling,
                Translation = wordDto.Translation,
                Gender = wordDto.Gender!.Value.ToEntity(context)
            },
            WordEnum.Verb => new Verb
            {
                Id = wordDto.Id,
                Spelling = wordDto.Spelling,
                Translation = wordDto.Translation
            },
            WordEnum.Adjective => new Adjective
            {
                Id = wordDto.Id,
                Spelling = wordDto.Spelling,
                Translation = wordDto.Translation
            },
            WordEnum.Adverb => new Adverb
            {
                Id = wordDto.Id,
                Spelling = wordDto.Spelling,
                Translation = wordDto.Translation
            },
            _ => new Misc
            {
                Id = wordDto.Id,
                Spelling = wordDto.Spelling,
                Translation = wordDto.Translation
            },
        };
    }

    public static WordDto ToDto(this Word word)
    {
        return word switch
        {
            Noun noun => new WordDto(word.Id, word.Spelling, word.Translation, WordEnum.Noun, noun.Gender.ToEnum()),
            Verb verb => new WordDto(word.Id, word.Spelling, word.Translation, WordEnum.Verb),
            Adjective adjective => new WordDto(word.Id, word.Spelling, word.Translation, WordEnum.Adjective),
            Adverb adverb => new WordDto(word.Id, word.Spelling, word.Translation, WordEnum.Adverb),
            _ => new WordDto(word.Id, word.Spelling, word.Translation, WordEnum.Misc)
        };
    }
}