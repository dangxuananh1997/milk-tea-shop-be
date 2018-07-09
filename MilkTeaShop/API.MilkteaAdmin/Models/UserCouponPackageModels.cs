using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.MilkteaAdmin.Models
{
    public class UserCouponPackageVM
    {
        public int Id { get; set; }
        public int CouponPackageId { get; set; }
        public int DrinkQuantity { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public DateTime PurchasedDate { get; set; }
    }

    public class UserCouponPackageCM
    {
        public int CouponPackageId { get; set; }
        public int DrinkQuantity { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public DateTime PurchasedDate { get; set; }
    }

    public class UserCouponPackageUM
    {
        public int Id { get; set; }
        public int CouponPackageId { get; set; }
        public int DrinkQuantity { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public DateTime PurchasedDate { get; set; }
    }
}