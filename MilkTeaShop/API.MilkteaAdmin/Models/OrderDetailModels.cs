using Core.ObjectModel.ConstantManager;
using Core.ObjectModel.Entity;
using System.ComponentModel.DataAnnotations;

namespace API.MilkteaAdmin.Models
{
    public class OrderDetailVM
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public ProductVariantODVM ProductVariant { get; set; }
    }

    public class OrderDetailCM
    {
        [Required]
        [RegularExpression(@"^(\d{1,5})\b", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int ProductVariantId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal UnitPrice { get; set; }
    }

    public class OrderDetailUM
    {
        [Required]
        [RegularExpression(@"^(\d{1,5})\b", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductVariantId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal UnitPrice { get; set; }
    }
}