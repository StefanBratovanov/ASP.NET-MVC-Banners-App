﻿namespace Banners.Web.Models
{
    using System;
    using System.Linq.Expressions;
    using Banners.Models;

    public class BannerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] ImageData { get; set; }

        public static Expression<Func<Banner, BannerViewModel>> ViewModel
        {
            get
            {
                return b => new BannerViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    ImageData = b.ImageData,
                };
            }
        }
    }
}
