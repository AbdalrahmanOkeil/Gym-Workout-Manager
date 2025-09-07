using GYM.Models;
using System.Linq.Expressions;

namespace GYM.Repository
{
    public interface IRoutineRepository
    {
        public void Add(Routine obj);
        public void Update(Routine obj);
        public void Delete(int id);
        public IQueryable<Routine> GetAll();
        public Routine GetById(int id, params Expression<Func<Routine, object>>[] includes);
        public Task SaveAsync();
        public Routine GetByIdWithDetails(int id);
    }
}
