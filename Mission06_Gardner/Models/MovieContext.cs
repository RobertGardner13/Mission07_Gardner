using Microsoft.EntityFrameworkCore;

namespace Mission06_Gardner.Models;

public class MovieContext : DbContext
{
    public MovieContext(DbContextOptions<MovieContext> options) : base(options)
    {
        
    }
    
    public DbSet<NewMovie> NewMovies { get; set; }
}