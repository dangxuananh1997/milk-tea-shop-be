namespace API.MilkteaClient.Models
{
    using Core.ObjectModel.Entity;

    public class ProductVariantVM
    {
        public int Id { get; set; }
        //public string ProductName { get; set; }
        public Size Size { get; set; }
        public decimal Price { get; set; }
    }

    // Product Variant in OrderDetailVM
    public class ProductVariantODVM
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public Size Size { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
    }

    public class ProductVariantCM
    {
        public int ProductId { get; set; }
        public Size Size { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductVariantUM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Size Size { get; set; }
        public decimal Price { get; set; }
    }
}