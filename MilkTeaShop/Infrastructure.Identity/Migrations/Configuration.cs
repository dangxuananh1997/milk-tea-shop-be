namespace Infrastructure.Identity.Migrations
{
    using Core.AppService.Business;
    using Core.ObjectModel.Entity;
    using global::Service.Business.Business;
    using Infrastructure.Entity.Database;
    using Infrastructure.Entity.Repositories;
    using Infrastructure.Identity.Model;
    using Infrastructure.Identity.Service;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;

    internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.Identity.Database.IdentityContext>
    {
        private IUserService _userService = new UserService(new UnitOfWork(new MilkteaProvider()));

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Infrastructure.Identity.Database.IdentityContext context)
        {
            var roleManager = new RoleService(new RoleStore<Role>(context));
            var userManager = new AccountService(new UserStore<Account>(context));

            if (!roleManager.RoleExists("Administrator"))
            {

                // first we create Admin rool   
                var role = new Role();
                role.Id = ((int)UserType.Administrator).ToString();
                role.Name = UserType.Administrator.ToString();
                roleManager.Create(role);

                role = new Role();
                role.Id = ((int)UserType.Guess).ToString();
                role.Name = UserType.Guess.ToString();
                roleManager.Create(role);

                role = new Role();
                role.Id = ((int)UserType.Member).ToString();
                role.Name = UserType.Member.ToString();
                roleManager.Create(role);

                role = new Role();
                role.Id = ((int)UserType.Shipper).ToString();
                role.Name = UserType.Shipper.ToString();
                roleManager.Create(role);

                
                //Here we create a Admin super user who will maintain the website                  

                //var user = new Account();
                //user.UserName = "duong@gmail.com";
                //user.Email = "duong@gmail.com";
                //user.UserType = UserType.Administrator;
                //string userPWD = "123456";

                //var chkUser = userManager.Create(user, userPWD);

                //if (chkUser.Succeeded)
                //{
                //    userManager.AddToRole(user.Id, UserType.Administrator.ToString());
                //}

                //for (int i = 1; i <= 10; i++)
                //{
                //    user = new Account();
                //    user.UserName = "quan" + i + "@gmail.com";
                //    user.Email = "quan" + i + "@gmail.com";
                //    user.UserType = UserType.Member;
                //    userPWD = "123456";

                //    chkUser = userManager.Create(user, userPWD);

                //    if (chkUser.Succeeded)
                //    {
                //        userManager.AddToRole(user.Id, UserType.Member.ToString());
                //    }
                //}
                
            }

            // Here we create a Admin super user who will maintain the website

            var user = new Account();
            user.UserName = "admin";
            //user.Email = "duong@gmail.com";
            user.UserType = UserType.Administrator;
            string userPWD = "123456";

            var chkUser = userManager.Create(user, userPWD);

            if (chkUser.Succeeded)
            {
                userManager.AddToRole(user.Id, UserType.Administrator.ToString());
            }

            // Create initial member
            Random rd = new Random();
            for (int i = 1; i <= 5; i++)
            {
                string[] prefix = { "09","016", "018" };
                user = new Account();
                user.UserName = prefix[rd.Next(prefix.Length - 1)] + rd.Next(10000000, 99999999);
                user.PhoneNumber = user.UserName;
                user.UserType = UserType.Member;
                userPWD = "123456";

                chkUser = userManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, UserType.Member.ToString());

                    _userService.CreateUser(new User
                    {
                        Username = user.UserName,
                        Phone = user.UserName,
                    });
                    _userService.SaveUserChanges();
                }
            }

            // Create initial guess
            for (int i = 1; i <= 5; i++)
            {
                string[] prefix = { "09", "016", "018" };
                user = new Account();
                user.UserName = prefix[rd.Next(prefix.Length - 1)] + rd.Next(10000000, 99999999);
                user.PhoneNumber = user.UserName;
                user.UserType = UserType.Member;
                userPWD = "123456";

                chkUser = userManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, UserType.Guess.ToString());

                    _userService.CreateUser(new User
                    {
                        Username = user.UserName,
                        Phone = user.UserName,
                    });
                    _userService.SaveUserChanges();
                }
            }
        }
    }
}
