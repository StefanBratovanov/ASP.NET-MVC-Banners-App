namespace Banners.Web.Controllers
{
    using System.Web.Mvc;
    using Banners.Data;

    public class BaseController : Controller
    {
        protected BannersDbContext db = new BannersDbContext();

    }
}