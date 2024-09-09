using TestWebApp.Services.WordsService.Models;

namespace TestWebApp.Services.LearningService.LearningCategories.Models;

public record GradeDto(
    WordDto Word,
    int UserId,
    int Grade,
    bool IsCorrect
);