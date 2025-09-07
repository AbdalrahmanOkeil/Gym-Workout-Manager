using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYM.Models
{
    public class RoutineExercise
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Routine")]
        public int RoutineId { get; set; }
        public Routine Routine { get; set; }

        [Required]
        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public string? Note {  get; set; }
        public string? RestTimer { get; set; }

        public ICollection<ExerciseSet> Sets { get; set; } = new List<ExerciseSet>();
    }
}
