namespace Banners.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Banners.Common;
    using Banners.Web.Models;
    using PagedList;

    public class HomeController : BaseController
    {
        public ActionResult Index(int? page)
        {
            var banners = this.db.Banners
                 .OrderBy(b => b.Id)
                 .Select(BannerViewModel.ViewModel)
                 .ToPagedList(page ?? GlobalConstants.DefaultPageNumber, GlobalConstants.DefaultPageSize);

            return this.View(banners);
        }
    }
}