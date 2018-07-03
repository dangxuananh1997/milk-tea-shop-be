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
        public int CouponItemId { get; set; }

        public User User { get; set; }
        public CouponItem CouponItem { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
