namespace Banners.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class BannerEditModel
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

        public byte[] ImageData { get; set; }

        [Display(Name = "Image:")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}