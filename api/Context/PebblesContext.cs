using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Pebbles.Models;

namespace Pebbles.Context;

public class PebblesContext : DbContext
{
    public DbSet<Patient> Patient { get; set; }
    public DbSet<Specialist> Specialist { get; set; }
    public DbSet<PatientSpecialist> PatientSpecialist { get; set; }
    public DbSet<MovementSuggestion> MovementSuggestion { get; set; }
    public DbSet<Avatar> Avatar { get; set; }
    public DbSet<Purchase> Purchase { get; set; }
    public DbSet<Color> Color { get; set; }
    public DbSet<Answer> Answer { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Option> Option { get; set; }
    public DbSet<Question> Question { get; set; }
    public DbSet<Questionnaire> Questionnaire { get; set; }
    public DbSet<Scale> Scale { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Login> Login { get; set; }

    private readonly IConfiguration _configuration;

    public PebblesContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(_configuration.GetConnectionString("PebblesDB"))
            .AddInterceptors(new SoftDeleteInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnonymousPatientData>().HasNoKey();

        modelBuilder.Entity<Purchase>()
            .HasKey(p => new { p.PatientId, p.ColorId });

        modelBuilder.Entity<PatientSpecialist>()
            .HasKey(ps => new { ps.PatientId, ps.SpecialistId });
        
        modelBuilder.Entity<QuestionnaireQuestion>()
            .HasKey(qq => new { qq.QuestionnaireId, qq.QuestionId });

        //relations between tables

        modelBuilder.Entity<Specialist>()
            .HasMany(s => s.Patients)
            .WithMany(p => p.Specialists)
            .UsingEntity<PatientSpecialist>();

        modelBuilder.Entity<Patient>()
            .HasMany(p => p.Colors)
            .WithMany(c => c.Patients)
            .UsingEntity<Purchase>();
        modelBuilder.Entity<Patient>()
            .HasOne(p => p.Avatar)
            .WithOne(a => a.Patient)
            .HasForeignKey<Avatar>(a => a.PatientId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<PatientSpecialist>()
            .HasOne(ps => ps.Patient)
            .WithMany(p => p.PatientSpecialists)
            .HasForeignKey(ps => ps.PatientId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<PatientSpecialist>()
            .HasOne(ps => ps.Specialist)
            .WithMany(s => s.PatientSpecialists)
            .HasForeignKey(ps => ps.SpecialistId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Avatar>()
            .HasOne(a => a.Color)
            .WithMany(c => c.Avatars)
            .HasForeignKey(a => a.ColorId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Questionnaire>()
            .HasOne(q => q.Patient)
            .WithMany(p => p.Questionnaires)
            .HasForeignKey(q => q.PatientId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Questionnaire>()
            .HasOne(q => q.Specialist)
            .WithMany(s => s.Questionnaires)
            .HasForeignKey(q => q.SpecialistId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Questionnaire>()
            .HasMany(q => q.Questions)
            .WithMany(a => a.Questionnaires)
            .UsingEntity<QuestionnaireQuestion>();

        modelBuilder.Entity<Question>()
            .HasOne(q => q.Category)
            .WithMany(c => c.Questions)
            .HasForeignKey(q => q.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Question>()
            .HasOne(q => q.Scale)
            .WithMany(s => s.Questions)
            .HasForeignKey(q => q.ScaleId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Question>()
            .HasMany(q => q.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Option)
            .WithMany(o => o.Answers)
            .HasForeignKey(a => a.OptionId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Option>()
            .HasOne(o => o.Scale)
            .WithMany(q => q.Options)
            .HasForeignKey(o => o.ScaleId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<MovementSuggestion>()
            .HasOne(m => m.Patient)
            .WithMany(p => p.MovementSuggestions)
            .HasForeignKey(m => m.PatientId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<MovementSuggestion>()
            .HasOne(m => m.Specialist)
            .WithMany(s => s.MovementSuggestions)
            .HasForeignKey(m => m.SpecialistId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Login>()
            .HasOne(l => l.User)
            .WithMany(u => u.Logins)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<MovementSession>()
            .HasOne(m => m.Patient)
            .WithMany(p => p.MovementSessions)
            .HasForeignKey(m => m.PatientId)
            .OnDelete(DeleteBehavior.NoAction);

        //seed data
        modelBuilder.Entity<Specialist>().HasData(
            //specialists
            new Specialist("Walter", "De Pril", "walter.de.pril@ziekenhuis.be"),
            new Specialist("Johan", "Van der Auwera", "johan.van.der.auwera@ziekenhuis.be"),
            new Specialist("Rita", "Coonincks", "rita.coonincks@ziekenhuis.be")
        );

        modelBuilder.Entity<Color>().HasData(
            new Color("Blue (Default)", "#3B82F6")
        );
    }
}