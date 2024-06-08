using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication11.Models;

namespace WebApplication12.Models.ViewModels
{
    // tafavote in model ba Modele asli(Comment), faghat dar ine ke Display-Required-MaxLength va ... dare
    // mamolan ViewModel ha bakhshi az Model asli(Commnet) hastan. Partial model
    // chon ModelView be DB vasl nist, pas migration nemikhad
    public class CommentViewModel
    {
        [Display(Name="Comment Id")]
        public int commentId { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Name is Required"), MaxLength(100)]               // Required(ErrorMessage="")
        [Display(Name="Comment Text")]
        [DataType(DataType.MultilineText)]              // MultilineText. ckeditor
        public string commentText { get; set; }
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }
        [MaxLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email dosen't have correct format")]               // Type Email bashe
        public string Email { get; set; }
        [Display(Name="Register Date")]
        public DateTime RegisterDate { get; set; }
        [Required,Display(Name="Comment Status")]        //
        public bool IsActive { get; set; }
        [Required]
        [Display(Name="News Id")]
        public int NewsId { get; set; }

        //----------------------------------
        public virtual News News { get; set; }       // age virtual ro nanevisi, News Null miyare
    }
}