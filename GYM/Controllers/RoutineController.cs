using GYM.Models;
using GYM.Repository;
using GYM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GYM.Controllers
{
    [Authorize]
    public class RoutineController : Controller
    {
        private readonly IEquipmentRepository equipmentRepository;
        private readonly IMuscleRepository muscleRepository;
        private readonly IExerciseRepository exerciseRepository;
        private readonly IRoutineRepository routineRepository;

        public RoutineController(IEquipmentRepository equipmentRepository, IMuscleRepository muscleRepository
            , IExerciseRepository exerciseRepository, IRoutineRepository routineRepository)
        {
            this.equipmentRepository = equipmentRepository;
            this.muscleRepository = muscleRepository;
            this.exerciseRepository = exerciseRepository;
            this.routineRepository = routineRepository;
        }
        public IActionResult Index()
        {
            var routinesCount = routineRepository.GetAll().Where(r => r.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).Count();
            ViewBag.RoutinesCount = routinesCount;
            return View(routineRepository.GetAll().Where(r => r.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList());
        }

        public IActionResult Create()
        {
            var viewModel = new ExerciseInfoViewModel
            {
                Equipments = equipmentRepository.GetAll().ToList().Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList(),

                Muscles = muscleRepository.GetAll().ToList().Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList(),

                Exercises = exerciseRepository.GetAll().ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Save(RoutineDto model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Create");

            var routine = new Routine
            {
                Name = model.Title,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                RoutineExercises = model.Exercises.Select(ex => new RoutineExercise
                {
                    ExerciseId = ex.ExerciseId,
                    Note = ex.Note,
                    RestTimer = ex.RestTimer,
                    Sets = ex.Sets.Select(s => new ExerciseSet
                    {
                        SetIndex = s.SetIndex,
                        Kg = s.Kg,
                        Reps = s.Reps
                    }).ToList()
                }).ToList()
            };
            routineRepository.Add(routine);
            await routineRepository.SaveAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            routineRepository.Delete(id);
            await routineRepository.SaveAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var routine = routineRepository.GetByIdWithDetails(id);
            if(routine == null)
                return NotFound();

            return View(routine);
        }

        public IActionResult Edit(int id)
        {
            var routine = routineRepository.GetByIdWithDetails(id);
            var model = new EditViewModel
            {
                Equipments = equipmentRepository.GetAll().ToList().Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList(),

                Muscles = muscleRepository.GetAll().ToList().Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList(),

                Exercises = exerciseRepository.GetAll().ToList(),

                Routine = routine
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEdit(EditViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Edit", model);

            var routine = routineRepository.GetAll().Include(r => r.RoutineExercises).ThenInclude(re => re.Sets).FirstOrDefault(r => r.Id == model.Routine.Id);
            if (routine == null) return NotFound();

            routine.Name = model.Routine.Name;
            routine.Description = model.Routine.Description;
            //routine.RoutineExercises.Clear();
            foreach(var ex in model.Routine.RoutineExercises)
            {
                routine.RoutineExercises.Add(new RoutineExercise
                {
                    ExerciseId = ex.ExerciseId,
                    Note = ex.Note,
                    RestTimer = ex.RestTimer,
                    Sets = ex.Sets?.Select(s => new ExerciseSet
                    {
                        SetIndex = s.SetIndex,
                        Kg = s.Kg,
                        Reps = s.Reps
                    }).ToList() ?? new List<ExerciseSet>()
                });
            }
            await routineRepository.SaveAsync();
            return RedirectToAction("Index");
        }
    }
}
