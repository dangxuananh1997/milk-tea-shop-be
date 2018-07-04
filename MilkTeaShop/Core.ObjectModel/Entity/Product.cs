using System.Collections.Generic;

namespace Core.ObjectModel.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }

        public ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
