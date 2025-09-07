using GYM.Models;

namespace GYM.Repository
{
    public interface IUserRepository
    {
        public ApplicationUser GetUserById(string id);
    }
}
