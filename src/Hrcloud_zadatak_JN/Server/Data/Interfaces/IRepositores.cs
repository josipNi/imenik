using Imenik_JN.Server.Entities;

namespace Imenik_JN.Server.Data.Interfaces
{
    public interface IUserRepository : IEntityBaseRepository<User> { }
    public interface ITagRepository : IEntityBaseRepository<Tag> { }
    public interface IEmailRepository : IEntityBaseRepository<Email> { }
    public interface IPhoneRepository : IEntityBaseRepository<Phone> { }
}
