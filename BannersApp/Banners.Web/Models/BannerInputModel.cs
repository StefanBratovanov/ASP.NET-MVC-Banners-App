using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Banners.Web.Models
{
    public class BannerInputModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(200, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 1)]
        [Display(Name = "Name:")]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Valid From:")]
        public DateTime ValidFrom { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Valid Until:")]
        public DateTime ValidUntil { get; set; }

        [Display(Name = "Image:")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}

//public class BannerInputModel
//{
//    [Required(ErrorMessage = "Name is required")]
//    [StringLength(200, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 1)]
//    [Display(Name = "Name:")]
//    public string Name { get; set; }

//    [DataType(DataType.DateTime)]
//    [Display(Name = "Valid From:")]
//    public DateTime ValidFrom { get; set; }

//    [DataType(DataType.DateTime)]
//    [Display(Name = "Valid Until:")]
//    public DateTime ValidUntil { get; set; }

//    [Display(Name = "Image:")]
//    [Required(ErrorMessage = "Please select file")]
//    public HttpPostedFileBase File { get; set; }
//}