using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYM.Models
{
    public class Routine
    {
        public int Id { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [Required(ErrorMessage = "Routine Name Required!")]
        [StringLength(100)]
        [Display(Name = "Routine Name")]
        public string Name { get; set; }

        [StringLength(500)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        public ICollection<RoutineExercise> RoutineExercises { get; set; } = new List<RoutineExercise>();
    }
}
