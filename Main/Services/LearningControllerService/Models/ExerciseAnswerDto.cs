namespace TestWebApp.Services.LearningControllerService.Models;

public record ExerciseAnswerDto(
    ExerciseDto Exercise,
    string Answer
);