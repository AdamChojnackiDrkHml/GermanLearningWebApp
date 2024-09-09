using Microsoft.EntityFrameworkCore;
using TestWebApp.Data.Models.Genders;
using TestWebApp.Data.Models.Grades;
using TestWebApp.Data.Models.Users;
using TestWebApp.Data.Models.Words;

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
   
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<Word>().UseTptMappingStrategy();
      modelBuilder.Entity<Noun>().ToTable("NOUN");
      modelBuilder.Entity<Verb>().ToTable("VERB");
      modelBuilder.Entity<Adverb>().ToTable("ADVERB");
      modelBuilder.Entity<Adjective>().ToTable("ADJECTIVE");
      modelBuilder.Entity<Misc>().ToTable("MISC");

      modelBuilder.Entity<User>()
         .HasIndex(u => u.Username)
         .IsUnique();

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

   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
      optionsBuilder
         .UseLazyLoadingProxies();
   }

   public GermanLearningDbContext(DbContextOptions<GermanLearningDbContext> options)
      : base(options)
   {
   }
}
