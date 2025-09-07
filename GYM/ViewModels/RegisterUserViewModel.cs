using System.ComponentModel.DataAnnotations;

namespace GYM.ViewModels
{
    public class RegisterUserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage ="Password dosen`t match")]
        public string ConfirmPassword { get; set; }
    }
}
