using CSharpFunctionalExtensions;
using TestWebApp.Services.UserService.Models;

namespace TestWebApp.Services.UserService;

public interface IUserService
{
    public Task<Result> RegisterUser(UserDto userDto);
    
    public Task<Result> RegisterUserAndSetSessionUser(UserDto userDto);
    
    public Task<Result> LogInAndSetSessionUser(UserDto userDto);
    
    public Result<UserDto> GetSessionUserAsync();
}