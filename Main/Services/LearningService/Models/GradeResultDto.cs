namespace TestWebApp.Services.LearningService.Models;

public record GradeResultDto(
    bool IsCorrect,
    string CorrectAnswer
);
