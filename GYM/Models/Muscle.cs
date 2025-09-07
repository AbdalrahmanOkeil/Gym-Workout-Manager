using GYM.Models;

namespace GYM.Models
{
    public class Muscle
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Exercise> exercises { get; set; } = new List<Exercise>();
    }
}
