﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DataAccess.Models.Accounts
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
