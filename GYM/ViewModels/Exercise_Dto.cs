namespace GYM.ViewModels
{
    public class Exercise_Dto
    {
        public int ExerciseId { get; set; }
        public string? Note { get; set; }
        public string? RestTimer { get; set; }
        public List<SetDto>? Sets { get; set; } = new List<SetDto>();
    }

}
