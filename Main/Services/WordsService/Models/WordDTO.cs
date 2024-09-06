namespace TestWebApp.Services.WordsService.Models;

public record WordDto(
    int? Id,
    string Spelling,
    string Translation,
    WordEnum Type,
    GenderEnum? Gender = null
);