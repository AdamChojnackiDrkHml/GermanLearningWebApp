using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestWebApp.Data;
using TestWebApp.Data.Models.Users;
using TestWebApp.Services.UserService.Models;
using static TestWebApp.Extensions.ExceptionsExtensions;
using static TestWebApp.Extensions.SessionExtensions;

namespace TestWebApp.Services.UserService.Implementation;

public class UserService : IUserService
{
    private const string CredentialsFailedError = "Login failed due to incorrect username or password.";
    
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly GermanLearningDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(
        IHttpContextAccessor httpContextAccessor,
        GermanLearningDbContext context,
        IPasswordHasher<User> passwordHasher
    )
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> RegisterUser(UserDto userDto)
    {
        var userRes = userDto.ToEntity();

        var passwordHash = userRes.Map(user => _passwordHasher.HashPassword(user, user.Password));

        var user = passwordHash
            .Map(hash =>
                new User
                {
                    Username = userDto.UserName,
                    Password = hash
                }
            );

        if (user.IsFailure)
        {
            return Result.Failure(user.Error);
        }
        
        await _context.Users.AddAsync(user.Value);
        await _context.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result> RegisterUserAndSetSessionUser(UserDto userDto)
    {
        return await RegisterUser(userDto)
            .Map(async () => await LogInAndSetSessionUser(userDto));
    }

    public async Task<Result> LogInAndSetSessionUser(UserDto userDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userDto.UserName);

        if (user is null)
        {
            return Result.Failure(CredentialsFailedError);
        }

        if (userDto.HashedPassword is null)
        {
            return Result.Failure(CredentialsFailedError);
        }
        
        var verificationRes = WrapThrowable(() => _passwordHasher.VerifyHashedPassword(user, user.Password, userDto.HashedPassword))
            .Ensure(result => result != PasswordVerificationResult.Failed, CredentialsFailedError);
        
        if (verificationRes.IsFailure)
        {
            return Result.Failure(verificationRes.Error);
        }
        
        SetUserSession(user.ToDto());
        return Result.Success();
    }

    private void SetUserSession(UserDto user)
    { 
        _httpContextAccessor.HttpContext!.Session.SetObject("User", user);
    }

    public Result<UserDto> GetSessionUserAsync()
    {
        return _httpContextAccessor.HttpContext is null
            ? Result.Failure<UserDto>("No HTTP context available")
            : _httpContextAccessor.HttpContext!.Session.GetObject<UserDto>("User");
    }
}