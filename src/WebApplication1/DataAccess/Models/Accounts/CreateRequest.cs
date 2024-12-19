using System.ComponentModel.DataAnnotations;
using WebApplication1.Entities;

namespace WebApplication1.DataAccess.Models.Accounts
{
    public class CreateRequest
    {
        [Required]
        public string Title { get; set; }


        [Required]
        [EnumDataType(typeof(Role))]
        public string Role { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string NameSurname { get; set; }

        [Required]
        [Compare("NameSurname")]
        public string ConfirmPassword { get; set; }
    }
}
