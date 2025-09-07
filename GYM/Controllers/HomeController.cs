using System.Diagnostics;
using System.Threading.Tasks;
using GYM.Models;
using GYM.Repository;
using GYM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GYM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRoutineRepository routineRepository;
        private readonly IExerciseRepository exerciseRepository;

        public HomeController(ILogger<HomeController> logger, IRoutineRepository routineRepository, IExerciseRepository exerciseRepository)
        {
            _logger = logger;
            this.routineRepository = routineRepository;
            this.exerciseRepository = exerciseRepository;
        }

        public IActionResult LandingPage()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                viewModel.LatestRoutines = await routineRepository.GetAll().Where(x => x.UserId == userId).Take(3).ToListAsync();

                viewModel.LatestExercises =await exerciseRepository.GetAll().OrderByDescending(e=>e.Id).Take(3).ToListAsync();
            }
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
