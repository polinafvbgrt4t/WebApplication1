using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DataAccess.Models.Accounts
{
    public class AuthenticateRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string NameSurname { get; set; }
    }
}
