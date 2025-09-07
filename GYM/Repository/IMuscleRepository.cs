using GYM.Models;

namespace GYM.Repository
{
    public interface IMuscleRepository
    {
        public void Add(Muscle obj);
        public void Update(Muscle obj);
        public void Delete(int id);
        public List<Muscle> GetAll();
        public Muscle GetById(int id);
        public Task SaveAsync();
    }
}
