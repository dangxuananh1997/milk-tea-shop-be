using System.Collections.Generic;

namespace Core.ObjectModel.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<UserCouponPackage> UserCouponPackages { get; set; }
    }
}
