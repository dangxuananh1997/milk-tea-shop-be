namespace Service.Business.Business
{
    using Core.AppService.Business;
    using Core.ObjectService.Repositories;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<T> _repository;

        public BaseService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._repository = unitOfWork.GetRepository<T>();
        }

        protected virtual T Get(params object[] keys)
        {
            return this._repository.Get(keys);
        }

        protected virtual T Get(Expression<Func<T, bool>> predicated, params Expression<Func<T, object>>[] includes)
        {
            return this._repository.Get(predicated, includes);
        }

        protected virtual T GetAsNoTracking(Expression<Func<T, bool>> predicated, params Expression<Func<T, object>>[] includes)
        {
            return this._repository.GetAsNoTracking(predicated, includes);
        }

        protected virtual IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            return this._repository.GetAll(includes);
        }

        protected virtual void Create(T entity)
        {
            this._repository.Create(entity);
        }

        protected virtual void Update(T entity)
        {
            this._repository.Update(entity);
        }

        protected virtual void Delete(T entity)
        {
            this._repository.Delete(entity);
        }

        protected virtual void Delete(params object[] keys)
        {
            this._repository.Delete(keys);
        }

        protected virtual void SaveChanges()
        {
            this._unitOfWork.SaveChanges();
        }
    }
}
