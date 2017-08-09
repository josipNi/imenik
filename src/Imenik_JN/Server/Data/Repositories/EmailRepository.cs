using Imenik_JN.Server.Data.Interfaces;
using Imenik_JN.Server.Entities;

namespace Imenik_JN.Server.Data.Repositories
{
    public class EmailRepository : EntityBaseRepository<Email>, IEmailRepository
    {
        public EmailRepository(Imenik_DB_Context context) : base(context) { }
    }
}