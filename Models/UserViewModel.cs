using System;
using System.ComponentModel.DataAnnotations;

namespace endicott.Models
{
    public class UserViewModel : BaseEntity
    {
        [Display(Name = "User Name")]
        [Required]
        [MinLength(2)]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        // [Compare("password", ErrorMessage = "Password and confirmation must match")]
        public string Confirm { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
    }

    public class LoginViewModel : BaseEntity
    {
        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}