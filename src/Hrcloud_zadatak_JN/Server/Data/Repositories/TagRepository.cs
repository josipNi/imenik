using Imenik_JN.Server.Data.Interfaces;
using Imenik_JN.Server.Entities;

namespace Imenik_JN.Server.Data.Repositories
{
    public class TagRepository : EntityBaseRepository<Tag>, ITagRepository
    {
        public TagRepository(Hrcloud_DB_Context context) : base(context) { }
    }
}