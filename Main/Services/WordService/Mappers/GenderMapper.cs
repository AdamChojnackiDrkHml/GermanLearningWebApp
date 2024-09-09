using TestWebApp.Data;
using TestWebApp.Data.Models.Genders;
using TestWebApp.Services.WordService.Enums;

namespace TestWebApp.Services.WordService.Mappers;

public static class GenderMapper
{
    public static Gender ToEntity(this GenderEnum genderEnum, GermanLearningDbContext context)
    {
        return genderEnum switch
        {
            GenderEnum.Masculine => context.Genders.First(g => g.Name == GenderEnum.Masculine.ToString()),
            GenderEnum.Feminine => context.Genders.First(g => g.Name == GenderEnum.Feminine.ToString()),
            GenderEnum.Neutral => context.Genders.First(g => g.Name == GenderEnum.Neutral.ToString()),
            _ => throw new ArgumentOutOfRangeException(nameof(genderEnum), genderEnum, null)
        };
    }

    public static GenderEnum ToEnum(this Gender gender)
    {
        return gender.GenderId switch
        {
            (int)GenderType.Masculine => GenderEnum.Masculine,
            (int)GenderType.Feminine => GenderEnum.Feminine,
            (int)GenderType.Neutral => GenderEnum.Neutral,
            _ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
        };
    }
}