using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("PebblesDB"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //relations between tables
        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Question)
            .WithMany(q => q.Answers)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Option)
            .WithMany(o => o.Answers)
            .HasForeignKey(a => a.OptionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Questionnaire)
            .WithMany(q => q.Answers)
            .HasForeignKey(a => a.QuestionnaireId)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<Option>()
            .HasOne(s => s.Scale)
            .WithMany(o => o.Options)
            .HasForeignKey(o => o.ScaleId)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<Question>()
            .HasOne(q => q.Category)
            .WithMany(c => c.Questions)
            .HasForeignKey(q => q.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Question>()
            .HasOne(q => q.Scale)
            .WithMany(q => q.Questions)
            .HasForeignKey(q => q.ScaleId)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Colors)
            .WithMany(c => c.Users)
            .UsingEntity<Purchase>();


        modelBuilder.Entity<Avatar>()
            .HasOne(a => a.User)
            .WithMany(u => u.Avatars)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Avatar>()
            .HasOne(a => a.Color)
            .WithMany(c => c.Avatars)
            .HasForeignKey(a => a.ColorId)
            .OnDelete(DeleteBehavior.Restrict);


        //users with role id 1 are specialists, users with role id 0 are patients
        //there is a many to many relation between these two types of users using the UserSpecialist table
        //specify no action on delete

        modelBuilder.Entity<UserSpecialist>()
            .HasKey(us => new { us.UserId, us.SpecialistId });

        modelBuilder.Entity<UserSpecialist>()
            .HasOne(us => us.User)
            .WithMany(u => u.UserSpecialists)
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserSpecialist>()
            .HasOne(us => us.Specialist)
            .WithMany(s => s.SpecialistUsers)
            .HasForeignKey(us => us.SpecialistId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}