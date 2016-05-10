namespace Banners.Web.Models
{
    using System;
    using System.Linq.Expressions;
    using Banners.Models;

    public class ActiveBannerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] ImageData { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidUntil { get; set; }

        public static Expression<Func<Banner, ActiveBannerViewModel>> ViewModel
        {
            get
            {
                return b => new ActiveBannerViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    ImageData = b.ImageData,
                    ValidFrom = b.ValidFrom,
                    ValidUntil = b.ValidUntil
                };
            }
        }
    }
}
