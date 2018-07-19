using Core.ObjectModel.Entity;
using System;
using System.Collections.Generic;

namespace API.MilkteaClient.Models
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
        public decimal TotalPrice { get; set; }
        public int PaymentType { get; set; }
        public string ContactPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public string CustomerName { get; set; }

        public ICollection<int?> CouponItemIds { get; set; }
        public ICollection<OrderDetailCM> OrderDetails { get; set; }
    }

    public class OrderUM
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int PaymentType { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public string ContactPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public string CustomerName { get; set; }

        public ICollection<int?> CouponItemIds { get; set; }
        public ICollection<OrderDetailUM> OrderDetails { get; set; }
    }
}