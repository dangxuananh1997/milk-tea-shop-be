
namespace Infrastructure.Entity.Repositories
{
    using Core.ObjectService.Repositories;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = dbContext.Set<T>();
        }

        public void Create(T entity)
        {
            this._dbContext.Entry<T>(entity).State = EntityState.Added;
        }

        public void Delete(T entity)
        {
            if (this._dbContext.Entry<T>(entity).State == EntityState.Detached)
            {
                this._dbSet.Attach(entity);
            }

            this._dbContext.Entry<T>(entity).State = EntityState.Deleted;
        }

        public void Delete(params object[] keys)
        {
            T obj = this._dbSet.Find(keys);
            if (this._dbContext.Entry<T>(obj).State == EntityState.Detached)
            {
                this._dbSet.Attach(obj);
            }

            this._dbContext.Entry<T>(obj).State = EntityState.Deleted;
        }

        public T Get(params object[] keys)
        {
            return this._dbSet.Find(keys);
        }

        public T Get(Expression<System.Func<T, bool>> predicated, params Expression<System.Func<T, object>>[] includes)
        {
            return this.GetAll(includes).FirstOrDefault(predicated);
        }

        public T GetAsNoTracking(Expression<System.Func<T, bool>> predicated, params Expression<System.Func<T, object>>[] includes)
        {
            return this.GetAll(includes).AsNoTracking().FirstOrDefault(predicated);
        }

        public IQueryable<T> GetAll(params Expression<System.Func<T, object>>[] includes)
        {
            IQueryable<T> result = this._dbSet;

            foreach (var expression in includes)
            {
                result = result.Include(expression);
            }

            return result;
        }

        public void Update(T entity)
        {
            if (this._dbContext.Entry<T>(entity).State == EntityState.Detached)
            {
                this._dbSet.Attach(entity);
            }

            this._dbContext.Entry<T>(entity).State = EntityState.Modified;
        }
    }
}
