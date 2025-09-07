using GYM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYM.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Exercise name required!")]
        [StringLength(100,ErrorMessage ="Name must be less than 100 letters!")]
        [Display(Name="Exercise Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Name must be less than 500 letters!")]
        [Display(Name="Description")]
        public string? Description { get; set; }

        [Display(Name = "Equipment")]
        [ForeignKey("Equipment")]
        public int? EquipmentID { get; set; }
        public Equipment? Equipment { get; set; }

        [Display(Name = "Primary Muscle")]
        [ForeignKey("PrimaryMuscle")]
        public int PrimaryMuscleId { get; set; }
        public Muscle PrimaryMuscle { get; set; }

        [Display(Name = "Secondary Muscle")]
        [StringLength(50)]
        [ForeignKey("SecondaryMuscle")]
        public int? SecondaryMuscleId { get; set; }
        public Muscle SecondaryMuscle { get; set; }

        [Display(Name = "Video URL")]
        public string? VideoURL { get; set; }

        public ICollection<RoutineExercise> routineExercises { get; set; } = new List<RoutineExercise>();
    }
}
