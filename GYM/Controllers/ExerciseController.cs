using GYM.Models;
using GYM.Repository;
using GYM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GYM.Controllers
{
    [Authorize]
    public class ExerciseController : Controller
    {
        private readonly IExerciseRepository exerciseRepository;
        private readonly IEquipmentRepository equipmentRepository;
        private readonly IMuscleRepository muscleRepository;
        private readonly IWebHostEnvironment env;

        public ExerciseController(IExerciseRepository exerciseRepository, IEquipmentRepository equipmentRepository
            , IMuscleRepository muscleRepository, IWebHostEnvironment env)
        {
            this.exerciseRepository = exerciseRepository;
            this.equipmentRepository = equipmentRepository;
            this.muscleRepository = muscleRepository;
            this.env = env;
        }

        public IActionResult Index()
        {
            var viewModel = new ExerciseInfoViewModel
            {
                Equipments = equipmentRepository.GetAll().ToList().Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList(),

                Muscles = muscleRepository.GetAll().ToList().Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList(),

                Exercises = exerciseRepository.GetAll().ToList()
            };
            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new ExerciseFormViewModel
            {
                Equipments = equipmentRepository.GetAll().ToList().Select(e=>new SelectListItem { Value=e.Id.ToString(),Text=e.Name}).ToList(),

                Muscles = muscleRepository.GetAll().ToList().Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAdd(ExerciseFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exercise = new Exercise();
                    if (model.VideoFile != null && model.VideoFile.Length > 0)
                    {
                        var path = Path.Combine(env.WebRootPath, "exerciseVideos", model.VideoFile.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.VideoFile.CopyToAsync(stream);
                        }

                        exercise.VideoURL = "/exerciseVideos/" + model.VideoFile.FileName;
                    }
                    exercise.Name = model.Name;
                    exercise.Description = model.Description;
                    exercise.EquipmentID = model.EquipmentId;
                    exercise.PrimaryMuscleId = model.PrimaryMuscleId;
                    exercise.SecondaryMuscleId = model.SecondaryMuscleId;
                    exerciseRepository.Add(exercise);
                    await exerciseRepository.SaveAsync();

                    return RedirectToAction("Add");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            model.Equipments = equipmentRepository.GetAll().ToList().Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList();

            model.Muscles = muscleRepository.GetAll().ToList().Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList();
            return View("Add",model);
        }
        //Exercise/GetExerciseById?exerciseId=1
        public IActionResult GetExerciseById(int exerciseId)
        {
            var exercise = exerciseRepository.GetById(
                exerciseId,
                e => e.Equipment,
                e => e.PrimaryMuscle,
                e => e.SecondaryMuscle
            );

            if (exercise == null)
                return NotFound();

            var dto = new
            {
                Id = exercise.Id,
                Name = exercise.Name,
                VideoURL = exercise.VideoURL,
                Equipment = exercise.Equipment?.Name,
                PrimaryMuscle = exercise.PrimaryMuscle.Name,
                SecondaryMuscle = exercise.SecondaryMuscle?.Name
            };
            return Json(dto);
        }
        //Exercise/GetExerciseByEquipmentId?equipmentId=1
        public IActionResult GetExerciseByEquipmentId(int equipmentId)
        {
            var exercises = exerciseRepository.GetByEuipmentId(equipmentId, e => e.PrimaryMuscle);

            var dto = exercises.Select(e => new ExerciseDto
            {
                Id = e.Id,
                Name = e.Name,
                VideoURL = e.VideoURL,
                PrimaryMuscleName = e.PrimaryMuscle?.Name
            }).ToList();

            return Json(dto);
        }
        //Exercise/GetExerciseByMuscleId?muscleId=1
        public IActionResult GetExerciseByMuscleId(int muscleId)
        {
            var exercises=exerciseRepository.GetByMuscleId(muscleId, e => e.PrimaryMuscle);
            var dto = exercises.Select(e => new ExerciseDto
            {
                Id = e.Id,
                Name = e.Name,
                VideoURL = e.VideoURL,
                PrimaryMuscleName = e.PrimaryMuscle.Name
            }).ToList();
            return Json(dto);
        }
        //Exercise/SearchExercises?searchString=Dumbbell
        public IActionResult SearchExercises(string searchString)
        {
            var exercises = exerciseRepository.GetAll(e=>e.PrimaryMuscle);

            if (!string.IsNullOrEmpty(searchString))
            {
                exercises = exercises.Where(e => e.Name.ToLower().Contains(searchString.ToLower()));
            }
            var dto = exercises.Select(e => new ExerciseDto
            {
                Id = e.Id,
                Name = e.Name,
                VideoURL = e.VideoURL,
                PrimaryMuscleName = e.PrimaryMuscle.Name
            }).ToList();
            return Json(dto);
        }
    }
}
