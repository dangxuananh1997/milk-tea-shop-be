using Core.ObjectModel.Entity;
using System;
using System.Collections.Generic;

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

        public ICollection<CouponItemVM> CouponItems { get; set; }
        public ICollection<OrderDetailVM> OrderDetails { get; set; }
    }

    public class OrderCM
    {
        public decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public int UserId { get; set; }

        public ICollection<CouponItemVM> CouponItems { get; set; }
        public ICollection<OrderDetailCM> OrderDetails { get; set; }
    }

    public class OrderUM
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }

        public ICollection<CouponItemVM> CouponItems { get; set; }
        public ICollection<OrderDetailUM> OrderDetails { get; set; }
    }
}