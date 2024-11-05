using TestWebApp.Services.LearningService.Models;

namespace TestWebApp.Services.LearningControllerService.Models;

public record ExerciseDto(
    GradeDto WordToGrade,
    string QuestionWord
);