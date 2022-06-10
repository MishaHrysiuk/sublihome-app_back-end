using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sublihome.Data.Entities.Products
{
    [Table("ProductType")]
    public class ProductType
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
