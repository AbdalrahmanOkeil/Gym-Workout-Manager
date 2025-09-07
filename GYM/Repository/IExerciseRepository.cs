using GYM.Models;
using System.Linq.Expressions;

namespace GYM.Repository
{
    public interface IExerciseRepository
    {
        public void Add(Exercise obj);
        public void Update(Exercise obj);
        public void Delete(int id);
        public IQueryable<Exercise> GetAll(params Expression<Func<Exercise, object>>[] includes);
        public Exercise GetById(int id, params Expression<Func<Exercise, object>>[] includes);
        public Task SaveAsync();
        public List<Exercise> GetByEuipmentId(int equipmentId, params Expression<Func<Exercise, object>>[] includes);
        public List<Exercise> GetByMuscleId(int muscleId, params Expression<Func<Exercise, object>>[] includes);
    }
}
