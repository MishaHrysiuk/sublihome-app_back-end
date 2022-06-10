using System;
using System.Collections.Generic;
using System.Text;

namespace Sublihome.Application.Dto.Carts
{
    public class NewCartProductsDto
    {
        public int UserId { get; set; }

        public List<int> ProductsList { get; set; }

        public List<int> ProductsCount { get; set; }
    }
}
