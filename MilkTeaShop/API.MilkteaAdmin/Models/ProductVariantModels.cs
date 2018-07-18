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

    public class ProductVariantCM
    {
        [Required]
        [RegularExpression(@"\d{5}", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int ProductId { get; set; }

        [RegularExpression(@"[0-2]{1}", ErrorMessage = ErrorMessage.INVALID_SIZE)]
        public Size Size { get; set; }

        [Required]
        [DataType(DataType.Currency, ErrorMessage = ErrorMessage.INVALID_PRICE)]
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