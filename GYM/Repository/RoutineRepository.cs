using GYM.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GYM.Repository
{
    public class RoutineRepository : IRoutineRepository
    {
        private readonly ApplicationContext context;

        public RoutineRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Add(Routine obj)
        {
            context.Add(obj);
        }

        public void Delete(int id)
        {
            Routine routine = GetById(id);
            context.Remove(routine);
        }

        public IQueryable<Routine> GetAll()
        {
            return context.Routines;
        }

        public Routine GetById(int id, params Expression<Func<Routine, object>>[] includes)
        {
            IQueryable<Routine> query = context.Routines;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.FirstOrDefault(r => r.Id == id);
        }

        public Routine GetByIdWithDetails(int id)
        {
            return context.Routines
                .Include(r => r.RoutineExercises)
                .ThenInclude(re => re.Exercise)
                .ThenInclude(e=>e.PrimaryMuscle)
                .Include(r => r.RoutineExercises)
                .ThenInclude(re => re.Sets)
                .Include(r => r.User)
                .FirstOrDefault(r => r.Id == id);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Update(Routine obj)
        {
            context.Update(obj);
        }
    }
}
