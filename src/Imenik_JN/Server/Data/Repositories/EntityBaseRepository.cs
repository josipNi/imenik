using Imenik_JN.Server.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Imenik_JN.Server.Data.Repositories
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T>
            where T : class, IEntityBase, new()
    {

        private Imenik_DB_Context _context;
        #region Properties
        public EntityBaseRepository(Imenik_DB_Context context)
        {
            _context = context;
        }
        #endregion
        public virtual IQueryable<T> GetAll() => _context.Set<T>();

        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsQueryable();
        }

        public T GetSingle(int id) => _context.Set<T>().FirstOrDefault(x => x.Id == id);

        public async Task<T> GetSingleAsync(int id) => await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate) => await _context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate) => _context.Set<T>().Where(predicate);

        public virtual void DeleteAllNotInSet(List<T> dontDeleteList, List<T> allList)
        {
            if (!dontDeleteList.Any()) // there are no items delete all of old items.
            {
                foreach (var item in allList)
                {
                    EntityEntry dbEntityEntry = _context.Entry<T>(item);
                    dbEntityEntry.State = EntityState.Deleted;
                }
            }
            if (dontDeleteList.Any() && allList.Any())
            {
                List<T> itemsToDelete = allList.Except(dontDeleteList).ToList();
                foreach (var item in itemsToDelete)
                {
                    EntityEntry dbEntityEntry = _context.Entry<T>(item);
                    dbEntityEntry.State = EntityState.Deleted;
                }
            }
        }

        public async virtual void Add(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            // dbEntityEntry.State = EntityState.Added;
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> entities = _context.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                _context.Entry<T>(entity).State = EntityState.Deleted;
            }
        }

        public async Task SaveChangesAsync()
        {
            bool hasChanges = _context.ChangeTracker.Entries().Any(e => e.State == EntityState.Added || e.State == EntityState.Deleted || e.State == EntityState.Modified);
            if (hasChanges)
                await _context.SaveChangesAsync();
            else
                await Task.CompletedTask;
        }

        public void SaveChanges()
        {
            bool hasChanges = _context.ChangeTracker.Entries().Any(e => e.State == EntityState.Added || e.State == EntityState.Deleted || e.State == EntityState.Modified);
            if (hasChanges)
                _context.SaveChanges();
            else
                return;
        }
    }
}
