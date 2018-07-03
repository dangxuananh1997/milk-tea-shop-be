namespace Core.ObjectModel.Entity
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Size Size { get; set; }
        public decimal Price { get; set; }

        public Product Product { get; set; }

        public OrderDetail OrderDetail { get; set; }
    }
}
