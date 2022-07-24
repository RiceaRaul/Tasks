using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext DbContext { get; set; }

        public RepositoryBase(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        void IRepositoryBase<T>.Create(T entity)
        {
            DbContext.Set<T>().Add(entity);
        }

        void IRepositoryBase<T>.Delete(T entity)
        {
            DbContext.Set<T>().Remove(entity);
        }

        IQueryable<T> IRepositoryBase<T>.FindAll()
        {
            return DbContext.Set<T>().AsNoTracking();
        }

        IQueryable<T> IRepositoryBase<T>.FindByCondition(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().Where(expression).AsNoTracking();
        }

        void IRepositoryBase<T>.Update(T entity)
        {
            DbContext.Set<T>().Update(entity);
        }

        T IRepositoryBase<T>.FindById(int id)
        {
            return DbContext.Set<T>().Find(id);
        }


        IQueryable<T> IRepositoryBase<T>.IncludeOne(Expression<Func<T, object>> expression)
        {
            return DbContext.Set<T>().Include(expression);
        }

        public  IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            IIncludableQueryable<T, object> query = null;

            if(includes.Length > 0)
            {
                query = DbContext.Set<T>().Include(includes[0]);
            }
            for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
            {
                query = query.Include(includes[queryIndex]);
            }

            return query == null ? DbContext.Set<T>() : (IQueryable<T>)query;
        }

    }
}