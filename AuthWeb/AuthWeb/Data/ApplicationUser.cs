using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AuthWeb.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public  string SecurityQn1 { get; set; }
        [Required]
        public  string SecurityAns1 { get; set; }
        [Required]
        public  string SecurityQn2 { get; set; }
        [Required]
        public  string SecurityAns2 { get; set; }
        [Required]
        public  string SecurityQn3 { get; set; }
        [Required]
        public  string SecurityAns3 { get; set; }

        public static implicit operator string?(ApplicationUser? v)
        {
            throw new NotImplementedException();
        }
    }
}
