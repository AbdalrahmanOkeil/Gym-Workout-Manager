using GYM.Models;

namespace GYM.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly ApplicationContext context;

        public EquipmentRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public void Add(Equipment obj)
        {
            context.Add(obj);
        }

        public void Delete(int id)
        {
            Equipment equipment = GetById(id);
            context.Remove(equipment);
        }

        public List<Equipment> GetAll()
        {
            return context.Equipments.ToList();
        }

        public Equipment GetById(int id)
        {
            return context.Equipments.FirstOrDefault(e => e.Id == id);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Update(Equipment obj)
        {
            context.Update(obj);
        }
    }
}
