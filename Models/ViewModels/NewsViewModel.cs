using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication11.Models;

namespace WebApplication12.Models.ViewModels
{
    // NewsViewModel ro sakhtam ke faghat Display(Name="") dashte bashim. mishe partial model az modele asli(News model) bashe
    // mitunim hameye field haro nayarim
    // PK nemikhad chon be DB vasl nist. In model convert mishe to newsModel dar ayande
    // chon ModelView be DB vasl nist, pas migration nemikhad
    public class NewsViewModel
    {
        [Display(Name ="User Id")]
        public int NewsId { get; set; }
        [Required, MaxLength(100)]
        [Display(Name ="News Title")]
        public string NewsTitle { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]              // DataType bayad MultilineText(<textarea>) bashe ke Ckeditor kar kone
        [AllowHtml]                                     // Bayad AllowHtml behesh bedimm baraye Ckeditor
        public string Description { get; set; }
        [Display(Name ="Image Name")]
        public string ImageName { get; set; }
        [Display(Name = "Register Date")]
        [DisplayFormat(DataFormatString = "{0: dddd, dd MMMM YYYY}")]     // bayad  @Html.DisplayFor(model => item.RegisterDate)  ta fromat ro neshun bede
        public DateTime RegisterDate { get; set; }              
        [Display(Name ="Is Active")]
        public bool IsActive { get; set; }
        public int See { get; set; }
        public int Like { get; set; }
        
        [Display(Name ="NewsGroup Id")]
        public int NewsGroupId { get; set; }
        [Display(Name ="User Id")]
        public int UserId { get; set; }

        //----------------------------
        public User User { get; set; }
        public NewsGroup NewsGroup { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}