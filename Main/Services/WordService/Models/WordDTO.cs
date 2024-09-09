using TestWebApp.Services.WordService.Enums;

namespace TestWebApp.Services.WordService.Models;

public record WordDto(
    int? Id,
    string Spelling,
    string Translation,
    WordEnum Type,
    GenderEnum? Gender = null
);