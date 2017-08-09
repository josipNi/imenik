using Imenik_JN.Server.Data.Interfaces;
using Imenik_JN.Server.Entities;

namespace Imenik_JN.Server.Data.Repositories
{
    public class UserRepository: EntityBaseRepository<User>, IUserRepository
    {
        public UserRepository(Hrcloud_DB_Context context) : base(context) { }
    }
}