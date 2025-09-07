using GYM.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GYM.ViewModels
{
    public class ExerciseFormViewModel
    {
        [Required(ErrorMessage = "Exercise name required!")]
        [StringLength(100, ErrorMessage = "Name must be less than 100 letters!")]
        [Display(Name = "Exercise Name")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Name must be less than 500 letters!")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name="Equipment")]
        public int EquipmentId { get; set; }

        [Display(Name = "Primary Muscle")]
        public int PrimaryMuscleId { get; set; }

        [Display(Name = "Secondary Muscle")]
        public int? SecondaryMuscleId { get; set; }

        [Display(Name = "Upload Video")]
        public IFormFile VideoFile { get; set; }

        public IEnumerable<SelectListItem> Equipments { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Muscles { get; set; } = new List<SelectListItem>();
    }
}
