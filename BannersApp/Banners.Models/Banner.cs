namespace Banners.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Banner
    {
        public Banner()
        {
            this.IsActive = true;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public DateTime ValidFrom { get; set; }

        [Required]
        public DateTime ValidUntil { get; set; }

        public bool IsActive { get; set; }

        public byte[] ImageData { get; set; }

        public int ImageSize { get; set; }

        public string FileName { get; set; }
    }
}

