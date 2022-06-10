using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Sublihome.Data.Entities.Carts;
using Sublihome.Data.Entities.Orders;

namespace Sublihome.Data.Entities.Products
{
    [Table("Product")]
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        public ProductType ProductType { get; set; }

        public byte[] Picture { get; set; }

        public decimal Price { get; set; }

        public ICollection<CartProducts> CartProducts { get; set; }

        public ICollection<OrderProducts> OrderProducts { get; set; }
    }
}
