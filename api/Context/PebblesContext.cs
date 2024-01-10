using Microsoft.EntityFrameworkCore;
using Pebbles.Models;

namespace Pebbles.Context;

public class PebblesContext : DbContext
{
    public DbSet<Answer> Answer { get; set; }
    public DbSet<Avatar> Avatar { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Color> Color { get; set; }
    public DbSet<Option> Option { get; set; }
    public DbSet<Purchase> Purchase { get; set; }
    public DbSet<Question> Question { get; set; }
    public DbSet<Questionnaire> Questionnaire { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Scale> Scale { get; set; }
    public DbSet<User> User { get; set; }

    private readonly IConfiguration _configuration;

    public PebblesContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //get connection string from appsettings.json
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("PebblesDB"));
    }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    
  }
}