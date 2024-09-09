using TestWebApp.Services.UserService.Models;

namespace TestWebApp.Services.HashingPasswordService;

public interface IHashingPasswordService
{
    public Task<string> CreateUser(UserDto create);
    public Task<string> UserVerify(UserDto verify);
}