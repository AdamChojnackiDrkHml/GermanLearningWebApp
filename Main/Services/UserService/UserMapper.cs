using CSharpFunctionalExtensions;
using TestWebApp.Data.Models.Users;
using TestWebApp.Services.UserService.Models;

namespace TestWebApp.Services.UserService;

public static class UserMapper
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto(user.Id, user.Username, user.Password);
    }
    
    public static Result<User> ToEntity(this UserDto userDto, string? hashedPassword = null)
    {

        hashedPassword ??= userDto.HashedPassword;
        
        if (string.IsNullOrWhiteSpace(hashedPassword))
        {
            return Result.Failure<User>("Password is required");
        }
        
        return Result.Success(new User
        {
            Id = userDto.UserId ?? 0,
            Username = userDto.UserName,
            Password = hashedPassword
        });
        
    }
}