using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        IQueryable<T> IncludeOne(Expression<Func<T, object>> expression);

        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);

        T FindById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
