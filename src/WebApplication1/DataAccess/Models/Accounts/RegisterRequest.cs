using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DataAccess.Models.Accounts
{
    public class RegisterRequest
    {

        [Required]
        public int Id { get; set; }

    

       

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string NameSurname { get; set; }

        [Required]
        [Compare("NameSurname")]
        public string ConfirmNameSurname { get; set; }

        [Range(typeof(bool), "true", "true")]
        public bool AcceptTerms { get; set; }
    }
}
