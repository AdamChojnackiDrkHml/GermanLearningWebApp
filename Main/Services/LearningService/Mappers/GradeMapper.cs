using TestWebApp.Data;
using TestWebApp.Data.Models.Grades;
using TestWebApp.Services.LearningService.Models;
using TestWebApp.Services.WordService.Mappers;

namespace TestWebApp.Services.LearningService.Mappers;

public static class GradeMapper
{
    public static GradeDto ToDto(this Grade grade)
    {
        return new GradeDto(grade.Word.ToDto(), grade.Id, grade.UserId, grade.Value);
    }
    
    public static Grade ToEntity(this GradeDto gradeDto, GermanLearningDbContext context)
    {
        return new Grade
        {
            Id = gradeDto.GradeId,
            UserId = gradeDto.UserId,
            Value = gradeDto.Grade,
            Word = context.Words.FirstOrDefault(word => word.Id == gradeDto.Word.Id) ?? gradeDto.Word.ToEntity(context),
        };
    }
}