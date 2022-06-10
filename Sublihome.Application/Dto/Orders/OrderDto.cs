using System;
using System.Collections.Generic;
using System.Text;

namespace Sublihome.Application.Dto.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string StatusId { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
