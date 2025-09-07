using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace GYM.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<RoutineExercise> RoutineExercises { get; set; }
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<ExerciseSet> ExerciseSets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Exercise>()
                .HasOne(e => e.PrimaryMuscle)
                .WithMany() 
                .HasForeignKey(e => e.PrimaryMuscleId)
                .OnDelete(DeleteBehavior.Restrict);

            // علاقة SecondaryMuscle
            builder.Entity<Exercise>()
                .HasOne(e => e.SecondaryMuscle)
                .WithMany()
                .HasForeignKey(e => e.SecondaryMuscleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
