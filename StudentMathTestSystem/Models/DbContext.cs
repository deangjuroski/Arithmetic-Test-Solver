using Microsoft.EntityFrameworkCore;
using StudentMathTestSystem.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<StudentMathTestSystem.Models.ExamTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Teacher>()
            .HasMany(t => t.Students)
            .WithOne(s => s.Teacher)
            .HasForeignKey(s => s.TeacherID);

        modelBuilder.Entity<Student>()
            .HasMany(s => s.Exams)
            .WithOne(e => e.Student)
            .HasForeignKey(e => e.StudentID);

        modelBuilder.Entity<Exam>()
            .HasMany(e => e.Tasks)
            .WithOne(t => t.Exam)
            .HasForeignKey(t => t.ExamID);
    }
}
