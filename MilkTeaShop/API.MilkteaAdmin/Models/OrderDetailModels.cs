﻿using Core.ObjectModel.Entity;

namespace API.MilkteaAdmin.Models
{
    public class OrderDetailVM
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public ProductVariant ProductVariant { get; set; }
    }

    public class OrderDetailCM
    {
        public int OrderId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class OrderDetailUM
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}