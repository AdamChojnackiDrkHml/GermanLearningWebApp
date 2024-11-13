using TestWebApp.Data.Models.Users;

namespace TestWebApp.Services.UserService;

internal static class TestUser
{
    public static User TestUserEntity() => new()
    {
        Id = 1,
        Username = "TestUser",
        Password = "TestPassword"
    };

}