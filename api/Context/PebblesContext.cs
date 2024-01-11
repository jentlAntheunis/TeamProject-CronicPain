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
    public DbSet<Patient> Patient { get; set; }
    public DbSet<Purchase> Purchase { get; set; }
    public DbSet<Question> Question { get; set; }
    public DbSet<Questionnaire> Questionnaire { get; set; }
    public DbSet<Scale> Scale { get; set; }
    public DbSet<Specialist> Specialist { get; set; }
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
        modelBuilder.Entity<AnonymousPatientData>().HasNoKey();

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


        modelBuilder.Entity<Specialist>()
            .HasMany(s => s.Patients)
            .WithMany(p => p.Specialists)
            .UsingEntity<PatientSpecialist>(
                j => j
                    .HasOne(us => us.Patient)
                    .WithMany(p => p.PatientSpecialists)
                    .HasForeignKey(us => us.PatientId)
                    .OnDelete(DeleteBehavior.Restrict),
                j => j
                    .HasOne(us => us.Specialist)
                    .WithMany(s => s.PatientSpecialists)
                    .HasForeignKey(us => us.SpecialistId)
                    .OnDelete(DeleteBehavior.Restrict)
            );


        modelBuilder.Entity<Avatar>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Avatars)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Avatar>()
            .HasOne(a => a.Color)
            .WithMany(c => c.Avatars)
            .HasForeignKey(a => a.ColorId)
            .OnDelete(DeleteBehavior.Restrict);


        //seed data
        modelBuilder.Entity<Specialist>().HasData(
            //specialists
            new Specialist("Walter", "De Pril", "walter.de.pril@ziekenhuis.be"),
            new Specialist("Johan", "Van der Auwera", "johan.van.der.auwera@ziekenhuis.be"),
            new Specialist("Rita", "Coonincks", "rita.coonincks@ziekenhuis.be")
        );
    }
}