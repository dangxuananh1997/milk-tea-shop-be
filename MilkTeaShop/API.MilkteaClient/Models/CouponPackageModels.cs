namespace API.MilkteaClient.Models
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
        public string Name { get; set; }
        public int DrinkQuantity { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
    }

    public class CouponPackageUM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DrinkQuantity { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
    }
}