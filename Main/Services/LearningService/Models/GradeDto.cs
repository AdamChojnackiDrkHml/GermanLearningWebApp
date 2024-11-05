using TestWebApp.Services.WordService.Models;

namespace TestWebApp.Services.LearningService.Models;

public record GradeDto(
    WordDto Word,
    int UserId,
    int Grade
);