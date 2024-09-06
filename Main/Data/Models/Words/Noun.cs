using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestWebApp.Data.Models.Genders;

namespace TestWebApp.Data.Models.Words;

[Table("NOUN")]
public class Noun : Word
{
    [Required, ForeignKey("GenderId")]
    public virtual Gender Gender { get; init; }
    
    public int GenderId { get; init; }

    
    public Noun()
    {
    }
}
