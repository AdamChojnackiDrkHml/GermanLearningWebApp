using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWebApp.Data.Models.Users;

[Table("USER")]
public class User
{
    [Key]
    public int Id { get; init; }
    
    [Required]
    public string Username { get; init; }
    
    [Required]
    public string Password { get; init; }
}