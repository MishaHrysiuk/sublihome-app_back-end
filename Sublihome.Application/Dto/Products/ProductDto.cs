using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sublihome.Data.Entities.Products;

namespace Sublihome.Application.Dto.Products
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ProductType ProductType { get; set; }

        public FileContentResult ProductPicture { get; set; }
    }
}
