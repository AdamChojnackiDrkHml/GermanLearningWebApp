namespace TestWebApp.Services.LearningControllerService.Models;

public record ExerciseResultDto(
    bool IsCorrect,
    string CorrectAnswer
);
