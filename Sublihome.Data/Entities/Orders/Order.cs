using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Sublihome.Data.Entities.Users;

namespace Sublihome.Data.Entities.Orders
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int StatusId { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<OrderProducts> OrderProducts { get; set; }
    }
}
