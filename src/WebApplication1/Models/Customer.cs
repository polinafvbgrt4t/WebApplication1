using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
        }

        public int CustomerId { get; set; }
        public string NameSurname { get; set; } = null!;
        public string Email { get; set; }
        public string AddressCustomer { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
       
    }
}
