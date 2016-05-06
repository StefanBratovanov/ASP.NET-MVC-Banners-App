using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using AutoMapper;
using Banners.Models;
using Bookmarks.Common.Mappings;

namespace Banners.Web.Models
{
    public class BannerViewModel 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] ImageData { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidUntil { get; set; }

        public static Expression<Func<Banner, BannerViewModel>> ViewModel
        {
            get
            {
                return b => new BannerViewModel()
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
