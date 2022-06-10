using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sublihome.Data.Entities.Orders
{
    public class OrderStatus
    {
        public int Id { get; set; }

        public string Status { get; set; }
    }
}
