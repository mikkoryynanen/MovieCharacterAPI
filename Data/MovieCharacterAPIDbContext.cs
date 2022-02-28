using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MovieCharacterAPI.Data
{
    public class MovieCharacterAPIDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Franchise> Franchises { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=MovieCharacterAPIDb;Integrated Security=True;");
        //}

        public MovieCharacterAPIDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Characters

            modelBuilder.Entity<Character>().HasData(new Character
            {
                Id = 1,
                FullName = "Robert Downey Jr",
                Alias = "Iron man",
                Gender = "Male",
                CharacterPicture = "https://th.bing.com/th/id/R.a1493b8983c19f3158df354f1c0fe054?rik=t6iG5uRzMhaPjw&pid=ImgRaw&r=0"
            });

            modelBuilder.Entity<Character>().HasData(new Character
            {
                Id = 2,
                FullName = "Elijah Wood",
                Alias = "Frodo Baggins",
                Gender = "Male",
                CharacterPicture = "https://i.pinimg.com/736x/1b/93/84/1b9384b6de87ab45a1391d454bd695c5.jpg"
            });

            modelBuilder.Entity<Character>().HasData(new Character
            {
                Id = 3,
                FullName = "Roger Moore",
                Alias = "James Bond",
                Gender = "Male",
                CharacterPicture = "https://assets.mi6-hq.com/images/features/magazine-special4a.jpg"
            });

            // Movies

            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 1,
                MovieTitle = "Iron man",
                Genre = "Action",
                ReleaseYear = "2008",
                Director = "Jon Favreau",
                MoviePicture = "https://th.bing.com/th/id/R.dc6072bd82f5c534f7f7583f451a5534?rik=GqOVQyAWzMtbcw&pid=ImgRaw&r=0",
                Trailer = "https://www.youtube.com/watch?v=8ugaeA-nMTc",
                FranchiseId = 1
            });

            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 2,
                MovieTitle = "The fellowship of the ring",
                Genre = "Fantasy",
                ReleaseYear = "2001",
                Director = "Peter Jackson",
                MoviePicture = "https://th.bing.com/th/id/OIP.iu0nj0wNpcI0N-Pss_ihwQHaKw?pid=ImgDet&rs=1",
                Trailer = "https://www.youtube.com/watch?v=V75dMMIW2B4",
                FranchiseId = 2
            });

            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 3,
                MovieTitle = "Moonraker",
                Genre = "Action",
                ReleaseYear = "1979",
                Director = "Lewis Gilbert",
                MoviePicture = "https://th.bing.com/th/id/R.769e22f0f07bc67a3a5430a88d10dbd6?rik=eZJtqpB1%2b3MaJA&pid=ImgRaw&r=0",
                Trailer = "https://www.youtube.com/watch?v=KFOOjYU16KE",
                FranchiseId = 3
            });

            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 4,
                MovieTitle = "Octopussy",
                Genre = "Action",
                ReleaseYear = "1983",
                Director = "John Glen",
                MoviePicture = "https://i.pinimg.com/originals/79/b4/9b/79b49b9b10c0cf4c472167c60d65cd43.jpg",
                Trailer = "https://www.youtube.com/watch?v=q1hLWZzgZvU",
                FranchiseId = 3
            });

            // Franchises

            modelBuilder.Entity<Franchise>().HasData(new Franchise
            {
                Id = 1,
                Name = "Marvel cinematic universe",
                Description = "The films based on characters that appear in comic books published by Marvel Comics"
            });

            modelBuilder.Entity<Franchise>().HasData(new Franchise
            {
                Id = 2,
                Name = "The lord of the rings",
                Description = "Fantasy films based on the novel written by J.R.R. Tolkien"
            });

            modelBuilder.Entity<Franchise>().HasData(new Franchise
            {
                Id = 3,
                Name = "James Bond",
                Description = "A british secret agent working for MI6 under the codename 007"
            });

            modelBuilder.Entity<Character>()
                .HasMany(p => p.Movies)
                .WithMany(p => p.Characters)
                .UsingEntity<Dictionary<string, object>>(
                    "CharacterMovie",
                    r => r.HasOne<Movie>().WithMany().HasForeignKey("MoviesId"),
                    l => l.HasOne<Character>().WithMany().HasForeignKey("CharactersId"),
                    je =>
                    {
                        je.HasKey("MoviesId", "CharactersId");
                        je.HasData(
                            new { MoviesId = 1, CharactersId = 1 },
                            new { MoviesId = 2, CharactersId = 2 },
                            new { MoviesId = 3, CharactersId = 3 },
                            new { MoviesId = 4, CharactersId = 3 }
                        );
                    }
                );

        }
    }
}
