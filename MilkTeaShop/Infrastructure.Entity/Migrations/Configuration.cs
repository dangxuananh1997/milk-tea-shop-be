namespace Infrastructure.Entity.Migrations
{
    using Core.ObjectModel.Entity;
    using System;
    using System.Collections.Generic;
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
            Random rd = new Random();

            string[] milkteaNames = { "Hồng trà sữa", "Lục trà sữa", "Trà sữa Thái", "Trà sữa ô long", "Trà sữa thập cẩm", "Trà ô long", "Trà đen", "Trà xanh", "Sinh tố", "Milkshake" };

            // ADD PRODUCTS
            for (int i = 0; i < 10; i++)
            {
                context.Products.AddOrUpdate(x => x.Name, new Product()
                {
                    Name = milkteaNames[i]
                });
            }

            context.SaveChanges();

            // ADD PRODUCTS VARIANT
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
                    context.ProductVariants.AddOrUpdate();
                    context.ProductVariants.AddOrUpdate(x => new { x.ProductId, x.Size }, new ProductVariant()
                    {
                        ProductId = i,
                        Size = pvSize,
                        Price = (decimal)rd.Next(2, 5) * 10000,
                    });
                }
            }

            context.SaveChanges();

            //// ADD USERS
            //for (int i = 1; i <= 10; i++)
            //{
            //    context.User.AddOrUpdate(u => u.Username, new User
            //    {
            //        Username = "quan" + i + "@gmail.com",
            //    });
            //}
            //context.SaveChanges();

            // ADD COUPONPACKAGES
            for (int i = 1; i <= 10; i++)
            {
                context.CouponPackages.AddOrUpdate(c => c.Id, new CouponPackage
                {
                    Name = "Package " + i,
                    Price = (decimal) rd.Next(1, 5) * 100000,
                    DrinkQuantity = rd.Next(1, 5)
                });
            }
            context.SaveChanges();
            //// ADD USER COUPON PACKAGES
            //for (int i = 1; i <= 10; i++)
            //{
            //    context.UserCouponPackage.AddOrUpdate(_ => _.Id, new UserCouponPackage
            //    {
            //        UserId = i,
            //        CouponPackageId = i,
            //        DrinkQuantity = rd.Next(1, 5),
            //        Price = (decimal)rd.NextDouble(1, 5) * 1000000 + 500000,
            //        PurchasedDate = DateTime.Now
            //    });
            //}
            //context.SaveChanges();
            //// ADD ORDERS
            //for (int i = 1; i <= 10; i++)
            //{
            //    context.Orders.AddOrUpdate(o => o.Id, new Order
            //    {
            //        PaymentType = PaymentType.Cash,
            //        Status = "Pending",
            //        TotalPrice = (decimal)rd.NextDouble() * 1000000 + 500,
            //        UserId = 1,
            //        OrderDate = DateTime.Now.AddDays(rd.Next(1, 10))
            //    });
            //}
            //context.SaveChanges();

            //// ADD ORDERDETAILS
            //for (int i = 1; i <= 10; i++)
            //{
            //    for (int j = 1; j <= 10; j++)
            //    {
            //        context.OrderDetails.AddOrUpdate(o => o.Id, new OrderDetail
            //        {
            //            OrderId = i,
            //            ProductVariantId = j,
            //            Quantity = 1,
            //            UnitPrice = 111
            //        });
            //    }
            //}

            //context.SaveChanges();

        }
    }
}
