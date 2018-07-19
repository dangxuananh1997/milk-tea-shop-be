using System;
using System.Collections.Generic;

namespace Core.ObjectModel.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public string ContactPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public string CustomerName { get; set; }

        public User User { get; set; }
        public ICollection<CouponItem> CouponItems { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
