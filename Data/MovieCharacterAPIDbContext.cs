using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Models;
using System.Collections.Generic;

namespace MovieCharacterAPI.Data
{
    public class MovieCharacterAPIDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Franchise> Franchises { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=MovieCharacterAPIDb;Integrated Security=True;");
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
                CharacterPicture = ""
            });

            modelBuilder.Entity<Character>().HasData(new Character
            {
                Id = 2,
                FullName = "Elijah Wood",
                Alias = "Frodo Baggins",
                Gender = "Male",
                CharacterPicture = ""
            });

            modelBuilder.Entity<Character>().HasData(new Character
            {
                Id = 3,
                FullName = "Roger Moore",
                Alias = "James Bond",
                Gender = "Male",
                CharacterPicture = ""
            });

            // Movies

            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 1,
                MovieTitle = "Iron man",
                Genre = "Action",
                ReleaseYear = "2008",
                Director = "Jon Favreau",
                MoviePicture = "url",
                Trailer = "url",
                FranchiseId = 1
            });

            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 2,
                MovieTitle = "The fellowship of the ring",
                Genre = "Fantasy",
                ReleaseYear = "2001",
                Director = "Peter Jackson",
                MoviePicture = "url",
                Trailer = "url",
                FranchiseId = 2
            });

            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 3,
                MovieTitle = "Moonraker",
                Genre = "Action",
                ReleaseYear = "1979",
                Director = "Lewis Gilbert",
                MoviePicture = "url",
                Trailer = "url",
                FranchiseId = 3
            });

            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 4,
                MovieTitle = "Octopussy",
                Genre = "Action",
                ReleaseYear = "1983",
                Director = "John Glen",
                MoviePicture = "url",
                Trailer = "url",
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
