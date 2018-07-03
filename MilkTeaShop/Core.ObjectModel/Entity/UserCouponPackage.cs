using System;
using System.Collections.Generic;

namespace Core.ObjectModel.Entity
{
    public class UserCouponPackage
    {
        public int Id { get; set; }
        public int CouponPackageId { get; set; }
        public int DrinkQuantity { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public DateTime PurchasedDate { get; set; }

        public User User { get; set; }
        public CouponPackage CouponPackage { get; set; }
        public ICollection<CouponItem> CouponItems { get; set; }
    }
}
