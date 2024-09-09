namespace TestWebApp.Services.UserService.Models;

public record UserDto(
    int? UserId,
    string UserName,
    string? HashedPassword
)
{
    public UserDto(UserDto userDto, string hashedPassword) : this(userDto.UserId, userDto.UserName, hashedPassword)
    {
    }
}