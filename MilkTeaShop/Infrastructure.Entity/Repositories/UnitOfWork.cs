using Core.AppService.Database.MilkTea;
using Core.ObjectService.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Infrastructure.Entity.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private IDictionary<Type, object> _repositories;

        public UnitOfWork(IMilkteaContext dbContext)
        {
            this._dbContext = dbContext.GetContext() as DbContext;

            if (this._dbContext == null)
            {
                throw new ArgumentException();
            }

            this._repositories = new Dictionary<Type, object>();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            IRepository<T> repository;

            if (!this._repositories.ContainsKey(typeof(T)))
            {
                repository = new Repository<T>(this._dbContext);
                this._repositories.Add(typeof(T), repository);
            }
            else
            {
                repository = this._repositories[typeof(T)] as Repository<T>;
            }

            return repository;
        }

        public void SaveChanges()
        {
            this._dbContext.SaveChanges();
        }
    }
}
