namespace TestWebApp.Services.HashingPasswordService.Implementation;
//
// public class HashingPasswordService : IHashingPasswordService
// {
//     private readonly GermanLearningDbContext _context;
//
//     public HashingPasswordService(GermanLearningDbContext context)
//     {
//         _context = context;
//     }
//
//     public Task<string> CreateUser(UserDto create)
//     {
//         byte[] salt = GenerateSalt();
//     }
//
//     public Task<string> UserVerify(UserDto verify)
//     {
//         throw new NotImplementedException();
//     }
//
// }