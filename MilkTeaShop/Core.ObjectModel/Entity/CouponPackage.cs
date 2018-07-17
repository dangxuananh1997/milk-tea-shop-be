using System.Collections.Generic;

namespace Core.ObjectModel.Entity
{
    public class CouponPackage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DrinkQuantity { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }

        public ICollection<UserCouponPackage> UserCouponPackages { get; set; }
    }
}
