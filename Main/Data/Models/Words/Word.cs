using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWebApp.Data.Models.Words;

public abstract class Word
{
    [Key]
    public int? Id { get; init; }
    
    [Required]
    public string Spelling { get; init; }
    
    [Required]
    public string Translation { get; init; }
}
