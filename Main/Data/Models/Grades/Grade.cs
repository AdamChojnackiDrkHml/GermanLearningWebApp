using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestWebApp.Data.Models.Users;
using TestWebApp.Data.Models.Words;

namespace TestWebApp.Data.Models.Grades;

[Table("GRADE")]
public class Grade
{
    [Key]
    public int Id { get; init; }
    
    [Required]
    public int Value { get; init; }
    
    public int UserId { get; init; }
    
    [Required, ForeignKey("UserId")]
    public virtual User User { get; init; }

    public int WordId { get; init; }

    [Required, ForeignKey("WordId")]
    public virtual Word Word { get; init; }
}