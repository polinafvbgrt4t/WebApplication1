using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DataAccess.Models.Accounts
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
