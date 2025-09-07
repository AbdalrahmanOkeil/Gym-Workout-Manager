using GYM.Models;

namespace GYM.Repository
{
    public interface IEquipmentRepository
    {
        public void Add(Equipment obj);
        public void Update(Equipment obj);
        public void Delete(int id);
        public List<Equipment> GetAll();
        public Equipment GetById(int id);
        public Task SaveAsync();
    }
}
