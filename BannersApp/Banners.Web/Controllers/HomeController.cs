using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Banners.Web.Models;

namespace Banners.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var banners = this.db.Banners
                 .Where(b => b.IsActive)
                 .Select(BannerViewModel.ViewModel);

            return View(banners);
        }


    }
}