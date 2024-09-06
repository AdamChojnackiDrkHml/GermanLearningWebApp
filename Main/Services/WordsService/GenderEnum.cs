namespace TestWebApp.Services.WordsService;

public enum GenderEnum
{
    Masculine = 1,
    Feminine = 2,
    Neutral = 3
}

public static class GenderEnumExtensions
{
    public static string ToArticle(this GenderEnum genderEnum)
    {
        return genderEnum switch
        {
            GenderEnum.Masculine => "der",
            GenderEnum.Feminine => "die",
            GenderEnum.Neutral => "das",
            _ => throw new ArgumentOutOfRangeException(nameof(genderEnum), genderEnum, null)
        };
    }


}