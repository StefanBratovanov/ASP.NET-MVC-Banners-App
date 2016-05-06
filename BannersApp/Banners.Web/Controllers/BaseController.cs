using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Banners.Data;

namespace Banners.Web.Controllers
{
    public class BaseController : Controller
    {
        protected BannersDbContext db = new BannersDbContext();

    }
}