using Core.ObjectModel.ConstantManager;
using Core.ObjectModel.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.MilkteaAdmin.Models
{
    public class OrderVM
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public string ContactPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public string CustomerName { get; set; }

        public ICollection<CouponItemVM> CouponItems { get; set; }
        public ICollection<OrderDetailVM> OrderDetails { get; set; }
    }

    public class OrderCM
    {
        [Required]
        [Range(0, int.MaxValue)]
        public decimal TotalPrice { get; set; }

        [RegularExpression(@"[0-2]{1}", ErrorMessage = ErrorMessage.INVALID_PAYMENT_TYPE)]
        public int PaymentType { get; set; }

        [Required]
        [RegularExpression(@"^(\d{1,5})\b", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int UserId { get; set; }

        public string ContactPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public string CustomerName { get; set; }

        // re
        public ICollection<int?> CouponItemIds { get; set; }

        [Required]
        public ICollection<OrderDetailCM> OrderDetails { get; set; }
    }

    public class OrderUM
    {
        [Required]
        [RegularExpression(@"^(\d{1,5})\b", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int Id { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal TotalPrice { get; set; }

        [RegularExpression(@"[0-2]{1}", ErrorMessage = ErrorMessage.INVALID_PAYMENT_TYPE)]
        public int PaymentType { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [RegularExpression(@"^(\d{1,5})\b", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int UserId { get; set; }

        public string ContactPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public string CustomerName { get; set; }


        public ICollection<int?> CouponItemIds { get; set; }

        [Required]
        public ICollection<OrderDetailUM> OrderDetails { get; set; }
    }

    // Order Update Status Model
    public class OrderUSM
    {
        [Required]
        [RegularExpression(@"^(\d{1,5})\b", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; }
    }
}