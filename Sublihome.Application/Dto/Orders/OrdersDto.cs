﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sublihome.Application.Dto.Orders
{
    public class OrdersDto
    {
        public int Order { get; set; }

        public List<int> ProductIds { get; set; }

        public List<int> ProductsCount { get; set; }

        public decimal TotalPriceOfOrder { get; set; }
    }
}
