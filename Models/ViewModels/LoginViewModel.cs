using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;        // add

namespace WebApplication12.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="Mobile Number")]
        public string MobileNumber { get; set;}
        [Required]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set;}
        [Display(Name ="Rememeber Me")]
        public bool RememberMe { get; set;}
        public string returnUrl { get; set;}
    }
}