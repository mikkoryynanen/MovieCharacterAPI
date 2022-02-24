using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Models;

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
    }
}
