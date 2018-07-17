namespace API.MilkteaClient.Models
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
    }

    public class ProductCM
    {
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