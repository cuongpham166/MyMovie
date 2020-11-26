using Microsoft.EntityFrameworkCore;
using MyMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovie.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public DbSet<Genre> Genre { get; set; }
        public DbSet<Movie> Movie { get; set; }

        public DbSet<MovieGenre> MovieGenre { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<Movie>()
                .Property(b => b.Rating)
                .HasPrecision(3, 1);
            
            modelBuilder.Entity<MovieGenre>()
                .HasKey(t => new { t.MovieId, t.GenreId });

            modelBuilder.Entity<MovieGenre>()
                .HasOne(pt => pt.Genre)
                .WithMany(p => p.MovieGenres)
                .HasForeignKey(pt => pt.GenreId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(pt => pt.Movie)
                .WithMany(p => p.MovieGenres)
                .HasForeignKey(pt => pt.MovieId);
        }

    }
}
