using Core.ObjectModel.ConstantManager;
using System.ComponentModel.DataAnnotations;

namespace API.MilkteaAdmin.Models
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
    }

    public class ProductCM
    {
        [Required]
        [StringLength(192, MinimumLength = 5)]
        public string Name { get; set; }

        public string Picture { get; set; }
    }

    public class ProductUM
    {
        [Required]
        [RegularExpression(@"^(\d{1,5})\b", ErrorMessage = ErrorMessage.INVALID_ID)]
        public int Id { get; set; }

        [Required]
        [StringLength(192, MinimumLength = 5)]
        public string Name { get; set; }

        public string Picture { get; set; }
    }
}