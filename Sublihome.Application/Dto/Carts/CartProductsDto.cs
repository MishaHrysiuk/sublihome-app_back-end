using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sublihome.Application.Dto.Carts
{
    public class CartProductsDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public FileContentResult Picture { get; set; }

        public int Count { get; set; }
    }
}
