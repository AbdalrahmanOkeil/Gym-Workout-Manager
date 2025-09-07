using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GYM.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="*")]
        public String UserName { get; set; }

        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Display(Name = "Remember Me!!")]
        public bool RememberMe { get; set; }
    }
}
