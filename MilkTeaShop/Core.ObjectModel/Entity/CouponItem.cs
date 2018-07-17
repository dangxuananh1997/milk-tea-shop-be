using System;

namespace Core.ObjectModel.Entity
{
    public class CouponItem
    {
        public int Id { get; set; }
        public DateTime DateExpired { get; set; }
        public bool IsUsed { get; set; }
        public int UserPackageId { get; set; }
        public int? OrderId { get; set; }

        public Order Order { get; set; }
        public UserCouponPackage UserCouponPackage { get; set; }
    }
}
