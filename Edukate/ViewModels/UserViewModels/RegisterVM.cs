using System.ComponentModel.DataAnnotations;

namespace Edukate.ViewModels.UserViewModels
{
    public class RegisterVM
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(3)]
        public string UserName { get; set; }

        [Required, MinLength(3)]
        public string Fullname { get; set; }
        
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
