using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.MilkteaClient.Models
{
    public class UserCouponPackageVM
    {
        public int Id { get; set; }
        public int CouponPackageId { get; set; }
        public int DrinkQuantity { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}")]
        public DateTime PurchasedDate { get; set; }

        public List<CouponItemVM> CouponItems { get; set; }
    }

    public class UserCouponPackageCM
    {
        public int CouponPackageId { get; set; }
        //public int DrinkQuantity { get; set; }
        //public decimal Price { get; set; }
        //public int UserId { get; set; }
    }

    public class UserCouponPackageUM
    {
        public int Id { get; set; }
        public int CouponPackageId { get; set; }
        //public int DrinkQuantity { get; set; }
        //public decimal Price { get; set; }
        public int UserId { get; set; }
        //public DateTime PurchasedDate { get; set; }
    }
}