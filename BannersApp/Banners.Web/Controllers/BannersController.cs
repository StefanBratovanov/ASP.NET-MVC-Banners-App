
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Banners.Models;
using Banners.Web.Models;
using Microsoft.AspNet.Identity;

namespace Banners.Web.Controllers
{
    public class BannersController : BaseController
    {
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(BannerInputModel model)
        {

            if (model != null && this.ModelState.IsValid)
            {
                if (model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
                {
                    this.ModelState.AddModelError("ImageUpload", "This field is required");
                    //TODO:add notification
                    return this.RedirectToAction("Index", "Home");
                }

                byte[] data = new byte[model.ImageUpload.ContentLength];
                model.ImageUpload.InputStream.Read(data, 0, model.ImageUpload.ContentLength);

                var banner = new Banner
                {
                    Name = model.Name,
                    ValidFrom = model.ValidFrom,
                    ValidUntil = model.ValidUntil,
                    FileName = model.ImageUpload.FileName,
                    ImageSize = model.ImageUpload.ContentLength,
                    ImageData = data
                };

                this.db.Banners.Add(banner);
                this.db.SaveChanges();
                //TODO:add notification

                return this.RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var bannerToEdit = this.db.Banners
                .FirstOrDefault(b => b.Id == id);

            if (bannerToEdit == null)
            {
                //TODO:add notification
                return this.RedirectToAction("Index", "Home");
            }

            var model = new BannerEditModel
            {
                Name = bannerToEdit.Name,
                ValidFrom = bannerToEdit.ValidFrom,
                ValidUntil = bannerToEdit.ValidUntil,
                ImageData = bannerToEdit.ImageData
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BannerEditModel model)
        {
            var bannerToEdit = this.db.Banners
                .FirstOrDefault(b => b.Id == id);

            if (bannerToEdit == null)
            {
                //TODO:add notification
                return this.RedirectToAction("Index", "Home");
            }

            if (model != null && this.ModelState.IsValid)
            {
                byte[] data = new byte[model.ImageUpload.ContentLength];
                model.ImageUpload.InputStream.Read(data, 0, model.ImageUpload.ContentLength);

                bannerToEdit.Name = model.Name;
                bannerToEdit.ValidFrom = model.ValidFrom;
                bannerToEdit.ValidUntil = model.ValidUntil;
                bannerToEdit.FileName = model.ImageUpload.FileName;
                bannerToEdit.ImageSize = model.ImageUpload.ContentLength;
                bannerToEdit.ImageData = data;

                this.db.SaveChanges();
                return this.RedirectToAction("Index", "Home");
            }
            return this.View(model);
        }

    }
}

