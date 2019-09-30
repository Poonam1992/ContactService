using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;

namespace ContactServiceSolution.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        Task<int> SaveChanges();
        Task<bool> Remove(int id);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null, bool noTracking = false);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool noTracking = false);
        IDbContextTransaction BeginRepositoryTransaction();
        void CommitTransaction(IDbContextTransaction repositoryTransaction);
        void RollbackTransaction(IDbContextTransaction repositoryTransaction);
    }
}
