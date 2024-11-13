using Microsoft.EntityFrameworkCore;
using TestWebApp.Data.Models.Genders;
using TestWebApp.Data.Models.Grades;
using TestWebApp.Data.Models.Users;
using TestWebApp.Data.Models.Words;
using TestWebApp.Services.UserService;

namespace TestWebApp.Data;

public class GermanLearningDbContext : DbContext
{
   public DbSet<Gender> Genders { get; set; }

   public DbSet<Noun> Nouns { get; set; }
   public DbSet<Verb> Verbs { get; set; }
   public DbSet<Adverb> Adverbs { get; set; }
   public DbSet<Adjective> Adjectives { get; set; }
   public DbSet<Misc> Miscs { get; set; }
   public DbSet<Word> Words { get; set; }
   
   public DbSet<Grade> Grades { get; set; }
   public DbSet<User> Users { get; set; }
   
   public GermanLearningDbContext(DbContextOptions<GermanLearningDbContext> options)
      : base(options) {}
   
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      ModelWordsTables(modelBuilder);

      ModelAndInitUserData(modelBuilder);

      ModelAndInitGenderData(modelBuilder);
   }

   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
      optionsBuilder
         .UseLazyLoadingProxies();
   }
   
   public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
   {
      await AddGradesAsync(cancellationToken);
      
      return await base.SaveChangesAsync(cancellationToken);
   }

   private async Task AddGradesAsync(CancellationToken cancellationToken)
   {
      await AddGradesForNewWordsAsync(cancellationToken);
      await AddGradesForNewUsersAsync(cancellationToken);
   }
   
   private async Task AddGradesForNewWordsAsync(CancellationToken cancellationToken)
   {
      var newGrades = ChangeTracker.Entries().Where(e => e.State == EntityState.Added)
         .Where(e => e.Entity.GetType().BaseType == typeof(Word))
         .Select(e => e.Entity as Word)
         .ToList()
         .Where(w => w is not null)
         .Select(w => Users.Select(
            u => new Grade
            {
               User = u,
               Word = w!,
               Value = 0
            }))
         .SelectMany(g => g);
      
      await Grades.AddRangeAsync(newGrades, cancellationToken);
   }

   private async Task AddGradesForNewUsersAsync(CancellationToken cancellationToken)
   {
      var newGrades = ChangeTracker.Entries().Where(e => e.State == EntityState.Added)
         .Where(e => e.Entity.GetType() == typeof(User))
         .Select(e => e.Entity as User)
         .ToList()
         .Where(u => u is not null)
         .Select(user => Words.Select(
            w => new Grade
            {
               User = user!,
               Word = w,
               Value = 0
            }
         ))
         .SelectMany(g => g);
      
      await Grades.AddRangeAsync(newGrades, cancellationToken);
   }
   
   private static void ModelWordsTables(ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<Word>().UseTptMappingStrategy();
      modelBuilder.Entity<Noun>().ToTable("NOUN");
      modelBuilder.Entity<Verb>().ToTable("VERB");
      modelBuilder.Entity<Adverb>().ToTable("ADVERB");
      modelBuilder.Entity<Adjective>().ToTable("ADJECTIVE");
      modelBuilder.Entity<Misc>().ToTable("MISC");
   }
   
   private static void ModelAndInitGenderData(ModelBuilder modelBuilder)
   {

      modelBuilder.Entity<Gender>()
         .Property(g => g.GenderId);

      modelBuilder.Entity<Gender>().HasData(
         new Gender
         { 
            GenderId = (int)GenderType.Masculine,
            Name = GenderType.Masculine.ToString()   
         },
         new Gender
         { 
            GenderId = (int)GenderType.Feminine,
            Name = GenderType.Feminine.ToString()   
         },
         new Gender
         { 
            GenderId = (int)GenderType.Neutral,
            Name = GenderType.Neutral.ToString()
         }
      );
   }
   private static void ModelAndInitUserData(ModelBuilder modelBuilder)
   {

      modelBuilder.Entity<User>()
         .HasIndex(u => u.Username)
         .IsUnique();

      modelBuilder.Entity<User>()
         .HasData(TestUser.TestUserEntity());
   }

}
