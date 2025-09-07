using GYM.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GYM.ViewModels
{
    public class ExerciseInfoViewModel
    {
        public int? EquipmentId { get; set; }
        public int? MuscleId { get; set; }
        public List<SelectListItem> Equipments { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Muscles { get; set; } = new List<SelectListItem>();
        public List<Exercise>Exercises { get; set; }= new List<Exercise>();

        public string? SearchString { get; set; }
    }
}
