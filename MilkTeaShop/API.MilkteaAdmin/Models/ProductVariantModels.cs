namespace API.MilkteaAdmin.Models
{
    using Core.ObjectModel.Entity;
    using System.Collections.Generic;

    public class ProductVariantVM
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public Size Size { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductVariantCM
    {
        public string ProductId { get; set; }
        public Size Size { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductVariantUM
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public Size Size { get; set; }
        public decimal Price { get; set; }
    }
}