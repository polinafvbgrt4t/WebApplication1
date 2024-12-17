using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DataAccess.Models.Accounts
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
