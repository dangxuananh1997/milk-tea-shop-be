namespace API.MilkteaAdmin.Models
{
    using Core.ObjectModel.ConstantManager;
    using Core.ObjectModel.Entity;
    using System.ComponentModel.DataAnnotations;

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
        [Required]
        [RegularExpression(@"^(\d{1,5})\b", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int ProductId { get; set; }

        [RegularExpression(@"[0-2]{1}", ErrorMessage = ErrorMessage.INVALID_SIZE)]
        public int Size { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
    }

    public class ProductVariantUM
    {
        [Required]
        [RegularExpression(@"^(\d{1,5})\b", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^(\d{1,5})\b", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int ProductId { get; set; }

        [RegularExpression(@"[0-2]{1}", ErrorMessage = ErrorMessage.INVALID_SIZE)]
        public int Size { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
    }
}