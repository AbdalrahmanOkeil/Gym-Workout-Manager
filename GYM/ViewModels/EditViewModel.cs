using GYM.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GYM.ViewModels
{
    public class EditViewModel
    {
        public int? EquipmentId { get; set; }
        public int? MuscleId { get; set; }
        public List<SelectListItem> Equipments { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Muscles { get; set; } = new List<SelectListItem>();
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
        //public List<RoutineExercise> RoutineExercises { get; set; } = new();

        public string? SearchString { get; set; }
        public Routine Routine { get; set; }
    }
}
