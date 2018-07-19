using Core.ObjectModel.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

namespace Infrastructure.Entity.Database
{
    public class MilkteaContext : DbContext
    {
        public MilkteaContext() : base("MilkteaCnn")
        {

        }

        public DbSet<CouponItem> CouponItems { get; set; }
        public DbSet<CouponPackage> CouponPackages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserCouponPackage> UserCouponPackage { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CouponItem>().ToTable("CouponItem");
            modelBuilder.Entity<CouponItem>().HasKey(_ => _.Id);
            modelBuilder.Entity<CouponItem>().Property(_ => _.DateExpired);
            modelBuilder.Entity<CouponItem>().Property(_ => _.IsUsed);
            modelBuilder.Entity<CouponItem>().Property(_ => _.OrderId).IsOptional();

            modelBuilder.Entity<CouponPackage>().ToTable("CouponPackage");
            modelBuilder.Entity<CouponPackage>().HasKey(_ => _.Id);
            modelBuilder.Entity<CouponPackage>().Property(_ => _.Name);
            modelBuilder.Entity<CouponPackage>().Property(_ => _.DrinkQuantity);
            modelBuilder.Entity<CouponPackage>().Property(_ => _.Price);
            modelBuilder.Entity<CouponPackage>().Property(_ => _.Picture);

            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Order>().HasKey(_ => _.Id);
            modelBuilder.Entity<Order>().Property(_ => _.PaymentType);
            modelBuilder.Entity<Order>().Property(_ => _.TotalPrice);
            modelBuilder.Entity<Order>().Property(_ => _.Status);
            modelBuilder.Entity<Order>().Property(_ => _.OrderDate).IsOptional();
            modelBuilder.Entity<Order>().Property(_ => _.ContactPhone).IsOptional();
            modelBuilder.Entity<Order>().Property(_ => _.DeliveryAddress).IsOptional();
            modelBuilder.Entity<Order>().Property(_ => _.CustomerName).IsOptional();

            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetail");
            modelBuilder.Entity<OrderDetail>().HasKey(_ => _.Id);
            modelBuilder.Entity<OrderDetail>().Property(_ => _.Quantity);
            modelBuilder.Entity<OrderDetail>().Property(_ => _.UnitPrice);

            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Product>().HasKey(_ => _.Id);
            modelBuilder.Entity<Product>().Property(_ => _.Name);
            modelBuilder.Entity<Product>().Property(_ => _.Picture);

            modelBuilder.Entity<ProductVariant>().ToTable("ProductVariant");
            modelBuilder.Entity<ProductVariant>().HasKey(_ => _.Id);
            modelBuilder.Entity<ProductVariant>().Property(_ => _.Price);
            modelBuilder.Entity<ProductVariant>().Property(_ => _.Size);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().HasKey(_ => _.Id);
            modelBuilder.Entity<User>().Property(_ => _.FullName);
            modelBuilder.Entity<User>().Property(t => t.Username).IsRequired().HasMaxLength(192)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                            new IndexAnnotation(
                            new IndexAttribute("IX_Username", 1) { IsUnique = true }));
            modelBuilder.Entity<User>().Property(_ => _.Address);
            modelBuilder.Entity<User>().Property(_ => _.Phone).IsRequired().HasMaxLength(11)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                            new IndexAnnotation(
                            new IndexAttribute("IX_Phone", 2) { IsUnique = true })); ;
            modelBuilder.Entity<User>().Property(_ => _.Avatar);

            modelBuilder.Entity<UserCouponPackage>().ToTable("UserCouponPackage");
            modelBuilder.Entity<UserCouponPackage>().HasKey(_ => _.Id);
            modelBuilder.Entity<UserCouponPackage>().Property(_ => _.Price);
            modelBuilder.Entity<UserCouponPackage>().Property(_ => _.PurchasedDate);

            modelBuilder.Entity<CouponPackage>()
                .HasMany(_ => _.UserCouponPackages)
                .WithRequired(_ => _.CouponPackage)
                .HasForeignKey(_ => _.CouponPackageId);

            modelBuilder.Entity<UserCouponPackage>()
                .HasMany(_ => _.CouponItems)
                .WithRequired(_ => _.UserCouponPackage)
                .HasForeignKey(_ => _.UserPackageId);

            modelBuilder.Entity<User>()
                .HasMany(_ => _.UserCouponPackages)
                .WithRequired(_ => _.User)
                .HasForeignKey(_ => _.UserId);

            modelBuilder.Entity<User>()
                .HasMany(_ => _.Orders)
                .WithRequired(_ => _.User)
                .HasForeignKey(_ => _.UserId);

            modelBuilder.Entity<Order>()
                .HasMany(_ => _.OrderDetails)
                .WithRequired(_ => _.Order)
                .HasForeignKey(_ => _.OrderId);

            modelBuilder.Entity<Order>()
                .HasMany(_ => _.CouponItems)
                .WithOptional(_ => _.Order)
                .HasForeignKey(_ => _.OrderId);

            modelBuilder.Entity<ProductVariant>()
                .HasMany(_ => _.OrderDetails)
                .WithRequired(_ => _.ProductVariant)
                .HasForeignKey(_ => _.ProductVariantId);

            modelBuilder.Entity<Product>()
                .HasMany(_ => _.ProductVariants)
                .WithRequired(_ => _.Product)
                .HasForeignKey(_ => _.ProductId)
                .WillCascadeOnDelete(true);
        }
    }
}
