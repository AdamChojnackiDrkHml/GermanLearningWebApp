using TestWebApp.Data;
using TestWebApp.Data.Models.Grades;
using TestWebApp.Services.LearningService.LearningCategories.Models;
using TestWebApp.Services.WordsService.Mappers;

namespace TestWebApp.Services.LearningService.LearningCategories.Mappers;

public static class GradeMapper
{
    public static GradeDto ToDto(this Grade grade)
    {
        return new GradeDto(grade.Word.ToDto(), grade.UserId, grade.Value, true);
    }
    
    public static Grade ToEntity(this GradeDto gradeDto, GermanLearningDbContext context)
    {
        return new Grade
        {
            UserId = gradeDto.UserId,
            Value = gradeDto.Grade,
            Word = context.Words.FirstOrDefault(word => word.Id == gradeDto.Word.Id) ?? gradeDto.Word.ToEntity(context),
        };
    }
}