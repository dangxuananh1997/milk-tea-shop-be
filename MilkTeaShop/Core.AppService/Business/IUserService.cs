namespace Core.AppService.Business
{
    using Core.ObjectModel.Entity;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IUserService : IBaseService<User>
    {
        User GetUser(params object[] keys);

        User GetUser(Expression<Func<User, bool>> predicated, params Expression<Func<User, object>>[] includes);

        User GetUserAsNoTracking(Expression<Func<User, bool>> predicated, params Expression<Func<User, object>>[] includes);

        IQueryable<User> GetAllUser(params Expression<Func<User, object>>[] includes);

        void CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(User user);

        void DeleteUser(int userId);

        void SaveUserChanges();
    }
}
