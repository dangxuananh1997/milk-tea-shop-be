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
        [StringLength(192, MinimumLength = 5)]
        public string Name { get; set; }
        public string Picture { get; set; }
    }

    public class ProductUM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
    }
}