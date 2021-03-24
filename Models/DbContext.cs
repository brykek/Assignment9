using System;
using Microsoft.EntityFrameworkCore;

namespace Assignment3Movies.Models
{
    //this is the model that creates a movie model from the sqlite database
    public class MovieDbContext : DbContext
    {

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {

        }
        //book objects for the database
        public DbSet<movie> movies { get; set; }
    }
}
 