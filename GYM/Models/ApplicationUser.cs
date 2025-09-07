using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYM.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageURL { get; set; }
        public int? Age { get; set; }

        public string JoinDate = DateTime.Now.ToString("MMMM dd,yyyy");

        public ICollection<Routine> Routines { get; set; }
    }
}
