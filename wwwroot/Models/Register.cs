using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayPal.Models
{
    public class Register
    {
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid e-mail address")]
        [DisplayName("E-mail Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Verify Password")]
        [Compare("Password", ErrorMessage = "Your passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}