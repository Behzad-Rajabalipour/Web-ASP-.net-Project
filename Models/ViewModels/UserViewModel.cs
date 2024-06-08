using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication11.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApplication12.Models.ViewModels
{
    // chon ModelView be DB vasl nist, pas migration nemikhad
    public class UserViewModel
    {
        [Display(Name ="User Id")]
        public int UserId { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name ="Mobile Number")]
        public string MobileNumber { get; set; }
        [Required, MaxLength(100)]                // to yek line
        [DataType(DataType.Password)]           
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }
        [Required, MaxLength(100)]
        public string Description { get; set; }
        public string IsActive { get; set; }

        //-------------------------------
        public virtual ICollection<News> News { get; set; }     // age virtual ro nanevisi, News Null miyare
    }
}