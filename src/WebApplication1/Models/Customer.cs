using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Entities;

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
        [StringLength(100)]
        public string NameSurname { get; set; } = null!;
       
        public string Email { get; set; }
       
        public string AddressCustomer { get; set; } = null!;
        public bool  AcceptTerms { get; set; }
        public Role Role { get; set; }  
        public string? VerificationToken { get; set; }
        public DateTime? Verified {  get; set; }    
        public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;
        public string? ResetToken { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? PasswordReset {  get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public  List<RefreshToken> RefreshTokens { get; set; }



        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x=>x.Token == token) != null;
        }
       
    }
}
