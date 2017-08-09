using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Imenik_JN.Server.Data.Interfaces
{
    public interface IEntityBaseRepository<T> where T: class, new()
    {
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);       
        IQueryable<T> GetAll();
        Task<T> GetSingleAsync(int id);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T GetSingle(int id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        Task SaveChangesAsync();
        void SaveChanges();
        void DeleteAllNotInSet(List<T> deleteList, List<T> listOfAllitems);
    }
}
