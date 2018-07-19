using Core.ObjectModel.ConstantManager;
using System.ComponentModel.DataAnnotations;

namespace API.MilkteaAdmin.Models
{
    public class CouponPackageVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DrinkQuantity { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
    }

    public class CouponPackageCM
    {
        [Required]
        [StringLength(192, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int DrinkQuantity { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
        public string Picture { get; set; }
    }

    public class CouponPackageUM
    {
        [Required]
        [RegularExpression(@"^(\d{1,5})\b", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int Id { get; set; }

        [Required]
        [StringLength(192, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int DrinkQuantity { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        public string Picture { get; set; }
    }
}