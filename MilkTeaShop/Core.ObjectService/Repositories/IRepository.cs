
namespace Core.ObjectService.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T> where T : class
    {
        T Get(params object[] keys);

        T Get(Expression<Func<T, bool>> predicated, params Expression<Func<T, object>>[] includes);

        T GetAsNoTracking(Expression<Func<T, bool>> predicated, params Expression<Func<T, object>>[] includes);

        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(params object[] keys);
    }
}
