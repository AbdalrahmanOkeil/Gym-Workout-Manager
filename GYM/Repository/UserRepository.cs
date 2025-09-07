using GYM.Models;

namespace GYM.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext context;

        public UserRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public ApplicationUser GetUserById(string id)
        {
            return context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
