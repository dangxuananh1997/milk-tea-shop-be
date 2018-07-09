namespace Infrastructure.Identity.Migrations
{
    using Core.ObjectModel.Entity;
    using Infrastructure.Identity.Model;
    using Infrastructure.Identity.Service;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.Identity.Database.IdentityContext>
    {
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

                var user = new Account();
                user.UserName = "duong@gmail.com";
                user.Email = "duong@gmail.com";
                user.UserType = UserType.Administrator;
                string userPWD = "123456";

                var chkUser = userManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, UserType.Administrator.ToString());
                }
                
            }
        }
    }
}
