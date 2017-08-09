using Imenik_JN.Server.Data.Interfaces;
using Imenik_JN.Server.Entities;

namespace Imenik_JN.Server.Data.Repositories
{
    public class PhoneRepository : EntityBaseRepository<Phone>, IPhoneRepository
    {
        public PhoneRepository(Hrcloud_DB_Context context) : base(context) { }
    }
}