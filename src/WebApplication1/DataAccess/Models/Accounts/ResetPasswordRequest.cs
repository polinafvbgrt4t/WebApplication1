using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DataAccess.Models.Accounts
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [MinLength(6)]
        public string NameSurname { get; set; }

        [Required]
        [Compare("NameSurname")]
        public string ConfirmNameSurname { get; set; }
    }
}
