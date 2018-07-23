namespace API.MilkteaClient.Models
{
    using System;

    public class CouponItemVM
    {
        public int Id { get; set; }
        public int UserPackageId { get; set; }
        public DateTime DateExpired { get; set; }
        public bool IsUsed { get; set; }
        public int? OrderId { get; set; }

        public UserCouponPackageVM UserCouponPackage { get; set; }
    }

    public class CouponItemCM
    {
        public int UserPackageId { get; set; }
        public DateTime DateExpired { get; set; }
        public bool IsUsed { get; set; }
    }

    public class CouponItemUM
    {
        public int Id { get; set; }
        public int UserPackageId { get; set; }
        public DateTime DateExpired { get; set; }
        public bool IsUsed { get; set; }
        public int? OrderId { get; set; }
    }
}