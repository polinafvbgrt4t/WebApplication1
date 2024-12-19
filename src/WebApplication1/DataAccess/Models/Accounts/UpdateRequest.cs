using System.ComponentModel.DataAnnotations;
using WebApplication1.Entities;

namespace WebApplication1.DataAccess.Models.Accounts
{
    public class UpdateRequest
    {
        private string _nameSurname;
        private string _confirmNameSurname;
        private string _role;
        private string _email;


        [EnumDataType(typeof(Role))]
        public string Role
        {
            get => _role;
            set => _role = replaceEmptyWithNull(value);
        }

        [EmailAddress]
        public string Email
        {
            get => _email;
            set => _email = replaceEmptyWithNull(value);
        }

        [MinLength(6)]
        public string NameSurname
        {
            get => _nameSurname;
            set => _nameSurname = replaceEmptyWithNull(value);
        }

        [Compare("NameSurname")]
        public string ConfirmNameSurname
        {
            get => _confirmNameSurname;
            set => _confirmNameSurname = replaceEmptyWithNull(value);
        }

        // helpers
        private string replaceEmptyWithNull(string value)
        {
            // replace empty string with null to make field optional
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
