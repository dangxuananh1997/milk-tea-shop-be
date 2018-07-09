
namespace Infrastructure.Identity.Database
{
    using System.Data.Entity;
    using Infrastructure.Identity.Model;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class IdentityContext : IdentityDbContext<Account>
    {
        public IdentityContext() : base("MilkteaCnn")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Account>().Property(_ => _.UserType).IsRequired();
        }

        public static IdentityContext Create()
        {
            return new IdentityContext();
        }
    }
}
