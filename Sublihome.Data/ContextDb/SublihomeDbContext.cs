using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Sublihome.Data.Entities.Carts;
using Sublihome.Data.Entities.Orders;
using Sublihome.Data.Entities.Products;
using Sublihome.Data.Entities.Users;

namespace Sublihome.Data.ContextDb
{
    public class SublihomeDbContext : DbContext
    {
        public SublihomeDbContext(DbContextOptions<SublihomeDbContext> options)
        : base(options)
        {
            
        }

        public virtual DbSet<ProductType> ProductTypes { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Cart> Carts { get; set; }

        public virtual DbSet<CartProducts> CartProducts { get; set; }

        public virtual DbSet<OrderProducts> OrderProducts { get; set; }

        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
