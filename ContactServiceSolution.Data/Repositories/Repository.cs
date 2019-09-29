using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ContactServiceSolution.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ContactServiceSolution.Data.Repositories
{
   public  class Repository<T> : IRepository<T> where T:class
    {
        private const string nullExceptionMessage = "Entity cannot be null!";
        private readonly ContactDatabaseContext context;
        private DbSet<T> entities;

        public Repository(ContactDatabaseContext _context)
        {
            this.context = _context;
            entities = context.Set<T>();
        }

        public T Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nullExceptionMessage);

            entities.Add(entity);
            return entity;
        }

        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable();
        }

        public async Task<T> Get(int id)
        {
            //if (noTracking)
            //    return await entities.AsNoTracking()

            return await entities.FindAsync(id);
        }

        public async Task<int> SaveChanges()
        {
            try
            {
                var result = await context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nullExceptionMessage);

            entities.Update(entity);
            return entity;
        }

        public async Task<bool> Remove(int id)
        {
            var entity = await Get(id);
            if (entity != null)
            {
                entities.Remove(entity);
                var result = await SaveChanges();
                return (result > 0) ? true : false;
            }
            else
            {
                return false;
            }

        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null, bool noTracking = false)
        {
            if (noTracking)
                return await entities.AsNoTracking().CountAsync(predicate);

            return await entities.CountAsync(predicate);
        }

        public IDbContextTransaction BeginRepositoryTransaction()
        {
            return context.Database.BeginTransaction();
        }
        public void CommitTransaction(IDbContextTransaction repositoryTransaction)
        {
            repositoryTransaction.Commit();
        }
        public void RollbackTransaction(IDbContextTransaction repositoryTransaction)
        {
            repositoryTransaction.Rollback();
        }
    }
}
