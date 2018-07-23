using Core.ObjectModel.ConstantManager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.MilkteaAdmin.Models
{
    public class UserCouponPackageVM
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public int CouponPackageId { get; set; }
        public int DrinkQuantity { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public DateTime PurchasedDate { get; set; }

        public List<CouponItemCM> CouponItems { get; set; }
    }

    public class UserCouponPackageCM
    {
        [Required]
        [RegularExpression(@"^(\d{1,5})\b", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int CouponPackageId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int DrinkQuantity { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [RegularExpression(@"^(\d{1,5})\b", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int UserId { get; set; }
    }

    public class UserCouponPackageUM
    {
        [Required]
        [RegularExpression(@"^(\d{1,5})\b", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^(\d{1,5})\b", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int CouponPackageId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int DrinkQuantity { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [RegularExpression(@"^(\d{1,5})\b", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int UserId { get; set; }


        public DateTime PurchasedDate { get; set; }
    }
}