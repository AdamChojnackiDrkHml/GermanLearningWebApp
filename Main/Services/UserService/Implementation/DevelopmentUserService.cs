using CSharpFunctionalExtensions;
using TestWebApp.Services.UserService.Models;

namespace TestWebApp.Services.UserService.Implementation;

public class DevelopmentUserService : IUserService
{

    public Task<Result> RegisterUser(UserDto userDto) => throw new NotImplementedException();
    public Task<Result> RegisterUserAndSetSessionUser(UserDto userDto) => throw new NotImplementedException();
    public Task<Result> LogInAndSetSessionUser(UserDto userDto) => throw new NotImplementedException();
    public Result<UserDto> GetSessionUser()
    {
        return Result.Success(new UserDto(1, "TestUser", "TestPassword"));
    }
}