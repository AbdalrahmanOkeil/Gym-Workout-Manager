using GYM.Models;

namespace GYM.ViewModels
{
    public class HomeViewModel
    {
        public List<Routine> LatestRoutines { get; set; } = new List<Routine>();

        public List<Exercise> LatestExercises { get; set; } = new List<Exercise>();

    }
}
