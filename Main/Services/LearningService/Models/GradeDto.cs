using TestWebApp.Services.WordService.Models;

namespace TestWebApp.Services.LearningService.Models;

public record GradeDto(
    WordDto Word,
    int GradeId,
    int UserId,
    int Grade
);