using GYM.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GYM.Repository
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly ApplicationContext context;

        public ExerciseRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Add(Exercise obj)
        {
            context.Add(obj);
        }

        public void Delete(int id)
        {
            Exercise exercise = GetById(id);
            context.Remove(exercise);
        }

        public IQueryable<Exercise> GetAll(params Expression<Func<Exercise, object>>[] includes)
        {
            IQueryable<Exercise> query = context.Exercises;
            foreach(var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public List<Exercise> GetByEuipmentId(int equipmentId, params Expression<Func<Exercise, object>>[] includes)
        {
            IQueryable<Exercise> query = context.Exercises;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            if(equipmentId == 0)
            {
                return query.ToList();
            }
            return query.Where(e => e.EquipmentID == equipmentId).ToList();
        }

        public Exercise GetById(int id, params Expression<Func<Exercise, object>>[] includes)
        {
            IQueryable<Exercise> query = context.Exercises;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.FirstOrDefault(e => e.Id == id);
        }

        public List<Exercise> GetByMuscleId(int muscleId, params Expression<Func<Exercise, object>>[] includes)
        {
            IQueryable<Exercise> query = context.Exercises;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            if (muscleId == 0)
            {
                return query.ToList();
            }
            return query.Where(e => e.PrimaryMuscleId == muscleId).ToList();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Update(Exercise obj)
        {
            context.Update(obj);
        }
    }
}
