using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication11.Models;

// NewsGroupViewModel ro sakhtam ke faghat Display(Name=""), MaxLength() dashte bashim. mishe partial model az modele asli(NewsGroup model) bashe
// mitunim hameye field haro nayarim
// chon be DB migrate nashode pas be DB vasl nist, pas PK nemikhad
namespace WebApplication12.Models.ViewModels
{
    public class NewsGroupViewModel
    {
        [Display(Name = "News Group ID")]
        public int NewsGroupId { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "NewsGroup Title")]
        public string NewsGroupTitle { get; set; }
        [MaxLength(100)]
        [Display(Name = "Image")]
        public string ImageName { get; set; }
        public string Description { get; set; }
        //-------------------------------
        public ICollection<News> News { get; set; }
    }
}