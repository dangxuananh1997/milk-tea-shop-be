namespace Core.ObjectModel.Entity
{
    using System.Collections.Generic;

    public class ProductVariant
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Size Size { get; set; }
        public decimal Price { get; set; }

        public Product Product { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
