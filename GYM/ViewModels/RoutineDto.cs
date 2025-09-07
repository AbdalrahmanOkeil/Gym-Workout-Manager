namespace GYM.ViewModels
{
    public class RoutineDto
    {
        public string Title { get; set; }
        public List<Exercise_Dto>? Exercises { get; set; } = new List<Exercise_Dto>();
    }

}
