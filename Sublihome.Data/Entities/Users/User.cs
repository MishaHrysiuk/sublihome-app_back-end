using System;
using System.Collections.Generic;
using System.Text;
using Sublihome.Data.Entities.Carts;
using Sublihome.Data.Entities.Orders;

namespace Sublihome.Data.Entities.Users
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public Cart Cart { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
