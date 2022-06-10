using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Sublihome.Data.Entities.Products;
using Sublihome.Data.Entities.Users;

namespace Sublihome.Data.Entities.Carts
{
    [Table("Cart")]
    public class Cart
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<CartProducts> CartProducts { get; set; }
    }
}
