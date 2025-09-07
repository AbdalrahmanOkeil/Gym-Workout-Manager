using System.ComponentModel.DataAnnotations;

namespace GYM.ViewModels
{
    public class RoleViewModel
    {
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
    }
}
