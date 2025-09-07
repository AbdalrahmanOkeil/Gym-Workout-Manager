using GYM.Models;
using System.Threading.Tasks;

namespace GYM.Repository
{
    public class MuscleRepository : IMuscleRepository
    {
        private readonly ApplicationContext context;

        public MuscleRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public void Add(Muscle obj)
        {
            context.Add(obj);
        }

        public void Delete(int id)
        {
            Muscle muscle = GetById(id);
            context.Remove(muscle);
        }

        public List<Muscle> GetAll()
        {
            return context.Muscles.ToList();
        }

        public Muscle GetById(int id)
        {
            return context.Muscles.FirstOrDefault(m => m.Id == id);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Update(Muscle obj)
        {
            context.Update(obj);
        }
    }
}
