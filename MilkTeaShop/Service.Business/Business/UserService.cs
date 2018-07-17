using System;
using System.Linq;
using System.Linq.Expressions;
using Core.AppService.Business;
using Core.ObjectModel.Entity;
using Core.ObjectService.Repositories;

namespace Service.Business.Business
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void CreateUser(User user)
        {
            base.Create(user);
        }

        public void DeleteUser(User user)
        {
            base.Delete(user);
        }

        public void DeleteUser(int userId)
        {
            base.Delete(userId);
        }

        public IQueryable<User> GetAllUser(params Expression<Func<User, object>>[] includes)
        {
            return base.GetAll(includes);
        }

        public User GetUser(params object[] keys)
        {
            return base.Get(keys);
        }

        public User GetUser(Expression<Func<User, bool>> predicated, params Expression<Func<User, object>>[] includes)
        {
            return base.Get(predicated, includes);
        }

        public User GetUserAsNoTracking(Expression<Func<User, bool>> predicated, params Expression<Func<User, object>>[] includes)
        {
            return base.GetAsNoTracking(predicated, includes);
        }

        public void SaveUserChanges()
        {
            base.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            base.Update(user);
        }
    }
}
