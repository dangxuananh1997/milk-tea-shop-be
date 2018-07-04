namespace Infrastructure.Entity.Migrations
{
    using Core.ObjectModel.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.Entity.Database.MilkteaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Infrastructure.Entity.Database.MilkteaContext context)
        {
            string[] milkteaNames = { "Hồng trà sữa", "Lục trà sữa", "Trà sữa Thái", "Trà sữa ô long", "Trà sữa thập cẩm", "Trà ô long", "Trà đen", "Trà xanh", "Sinh tố", "Milkshake" };
            for (int i = 0; i < 10; i++)
            {
                context.Products.AddOrUpdate(x => x.Name, new Product()
                {
                    Name = milkteaNames[i]
                });
            }

            context.SaveChanges();

            Random rd = new Random();

            for (int i = 1; i <= 10; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Size pvSize;

                    switch (j)
                    {
                        case 0:
                            pvSize = Size.S;
                            break;
                        case 1:
                            pvSize = Size.M;
                            break;
                        case 2:
                            pvSize = Size.L;
                            break;
                        default:
                            pvSize = Size.S;
                            break;
                    }

                    context.ProductVariants.AddOrUpdate(x => x.Id, new ProductVariant()
                    {
                        ProductId = i,
                        Size = pvSize,
                        Price = (decimal)(rd.NextDouble() * 50000 + 30000)
                    });
                }
            }

            //context.ProductVariants.AddOrUpdate(x => x.Id, new ProductVariant()
            //{
            //    ProductId = 1,
            //    Size = Size.M,
            //    Price = (decimal)(rd.NextDouble() * 50000 + 30000)
            //});

            context.SaveChanges();
        }
    }
}
