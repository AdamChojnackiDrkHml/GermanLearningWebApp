﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestWebApp.Data;

#nullable disable

namespace TestWebApp.Migrations
{
    [DbContext(typeof(GermanLearningDbContext))]
    partial class GermanLearningDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TestWebApp.Data.Models.Genders.Gender", b =>
                {
                    b.Property<int>("GenderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenderId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenderId");

                    b.ToTable("GENDER");

                    b.HasData(
                        new
                        {
                            GenderId = 1,
                            Name = "Masculine"
                        },
                        new
                        {
                            GenderId = 2,
                            Name = "Feminine"
                        },
                        new
                        {
                            GenderId = 3,
                            Name = "Neutral"
                        });
                });

            modelBuilder.Entity("TestWebApp.Data.Models.Grades.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.Property<int>("WordId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WordId");

                    b.ToTable("GRADE");
                });

            modelBuilder.Entity("TestWebApp.Data.Models.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("USER");
                });

            modelBuilder.Entity("TestWebApp.Data.Models.Words.Word", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Spelling")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Translation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Words");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("TestWebApp.Data.Models.Words.Adjective", b =>
                {
                    b.HasBaseType("TestWebApp.Data.Models.Words.Word");

                    b.ToTable("ADJECTIVE", (string)null);
                });

            modelBuilder.Entity("TestWebApp.Data.Models.Words.Adverb", b =>
                {
                    b.HasBaseType("TestWebApp.Data.Models.Words.Word");

                    b.ToTable("ADVERB", (string)null);
                });

            modelBuilder.Entity("TestWebApp.Data.Models.Words.Misc", b =>
                {
                    b.HasBaseType("TestWebApp.Data.Models.Words.Word");

                    b.ToTable("MISC", (string)null);
                });

            modelBuilder.Entity("TestWebApp.Data.Models.Words.Noun", b =>
                {
                    b.HasBaseType("TestWebApp.Data.Models.Words.Word");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.HasIndex("GenderId");

                    b.ToTable("NOUN", (string)null);
                });

            modelBuilder.Entity("TestWebApp.Data.Models.Words.Verb", b =>
                {
                    b.HasBaseType("TestWebApp.Data.Models.Words.Word");

                    b.ToTable("VERB", (string)null);
                });

            modelBuilder.Entity("TestWebApp.Data.Models.Grades.Grade", b =>
                {
                    b.HasOne("TestWebApp.Data.Models.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestWebApp.Data.Models.Words.Word", "Word")
                        .WithMany()
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Word");
                });

            modelBuilder.Entity("TestWebApp.Data.Models.Words.Adjective", b =>
                {
                    b.HasOne("TestWebApp.Data.Models.Words.Word", null)
                        .WithOne()
                        .HasForeignKey("TestWebApp.Data.Models.Words.Adjective", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestWebApp.Data.Models.Words.Adverb", b =>
                {
                    b.HasOne("TestWebApp.Data.Models.Words.Word", null)
                        .WithOne()
                        .HasForeignKey("TestWebApp.Data.Models.Words.Adverb", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestWebApp.Data.Models.Words.Misc", b =>
                {
                    b.HasOne("TestWebApp.Data.Models.Words.Word", null)
                        .WithOne()
                        .HasForeignKey("TestWebApp.Data.Models.Words.Misc", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestWebApp.Data.Models.Words.Noun", b =>
                {
                    b.HasOne("TestWebApp.Data.Models.Genders.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestWebApp.Data.Models.Words.Word", null)
                        .WithOne()
                        .HasForeignKey("TestWebApp.Data.Models.Words.Noun", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("TestWebApp.Data.Models.Words.Verb", b =>
                {
                    b.HasOne("TestWebApp.Data.Models.Words.Word", null)
                        .WithOne()
                        .HasForeignKey("TestWebApp.Data.Models.Words.Verb", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
