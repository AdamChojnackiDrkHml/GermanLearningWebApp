using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestWebApp.Data.Models.Genders;

[Table("GENDER")]
public class Gender
{
    [Key]
    public int GenderId { get; init; }
    
    [Required]
    public string Name { get; init; }


    public Gender()
    {
    }
}