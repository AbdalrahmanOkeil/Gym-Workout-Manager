using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYM.Models
{
    public class ExerciseSet
    {
        public int Id { get; set; }

        [ForeignKey("RoutineExercise")]
        public int RoutineExerciseId { get; set; }
        public RoutineExercise RoutineExercise { get; set; }

        [Range(1, 20, ErrorMessage = "Sets must be between 1 and 20")]
        [Display(Name = "Number of sets")]
        public int SetIndex { get; set; }

        public double? Kg { get; set; }

        [Range(1, 20, ErrorMessage = "Reps must be between 1 and 100")]
        [Display(Name = "Number of reps")]
        public int? Reps { get; set; }
    }
}
