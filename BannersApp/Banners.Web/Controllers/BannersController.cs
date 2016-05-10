namespace Banners.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Banners.Common;
    using Banners.Models;
    using Banners.Web.Extensions;
    using Banners.Web.Models;
    using PagedList;

    public class BannersController : BaseController
    {
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(BannerInputModel model)
        {
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/png"
            };

            if (model != null && this.ModelState.IsValid)
            {
                if (model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
                {
                    this.ModelState.AddModelError("ImageUpload", "This field is required");
                    this.AddNotification("Adding banner failed, Image needed", NotificationType.ERROR);
                    return this.View();
                }
                if (!validImageTypes.Contains(model.ImageUpload.ContentType))
                {
                    this.ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
                    this.AddNotification("Please choose either a GIF, JPG or PNG image.", NotificationType.ERROR);
                    return this.View();
                }

                if (model.ValidUntil < model.ValidFrom)
                {
                    this.AddNotification("Valid Until must be later than Valid From", NotificationType.ERROR);
                    return this.View();
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
                this.AddNotification("Banner successfuly created", NotificationType.SUCCESS);

                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var bannerToEdit = this.db.Banners
                .FirstOrDefault(b => b.Id == id);

            if (bannerToEdit == null)
            {
                this.AddNotification("Can not load this banner", NotificationType.ERROR);
                return this.RedirectToAction("Index", "Home");
            }

            var model = new BannerEditModel
            {
                Name = bannerToEdit.Name,
                ValidFrom = bannerToEdit.ValidFrom,
                ValidUntil = bannerToEdit.ValidUntil,
                ImageData = bannerToEdit.ImageData
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BannerEditModel model)
        {
            var bannerToEdit = this.db.Banners
                .FirstOrDefault(b => b.Id == id);

            if (bannerToEdit == null)
            {
                this.AddNotification("Can not edit this banner", NotificationType.ERROR);
                return this.RedirectToAction("Index", "Home");
            }

            if (model.ValidUntil < model.ValidFrom)
            {
                this.AddNotification("Edting banner failed, Valid Until must be later than Valid From", NotificationType.ERROR);
                return this.RedirectToAction("Edit", "Banners");
            }

            if (this.ModelState.IsValid)
            {
                if (model.ImageUpload != null)
                {
                    byte[] data = new byte[model.ImageUpload.ContentLength];
                    model.ImageUpload.InputStream.Read(data, 0, model.ImageUpload.ContentLength);
                    bannerToEdit.ImageData = data;
                    bannerToEdit.FileName = model.ImageUpload.FileName;
                    bannerToEdit.ImageSize = model.ImageUpload.ContentLength;
                }

                bannerToEdit.Name = model.Name;
                bannerToEdit.ValidFrom = model.ValidFrom;
                bannerToEdit.ValidUntil = model.ValidUntil;

                this.db.SaveChanges();
                this.AddNotification("Banner successfuly edited", NotificationType.SUCCESS);
                return this.RedirectToAction("Index", "Home");
            }
            return this.View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int id)
        {
            var bannerToDelete = this.db.Banners
                .FirstOrDefault(b => b.Id == id);

            if (bannerToDelete == null)
            {
                this.AddNotification("Can not load this banner", NotificationType.ERROR);
                return this.RedirectToAction("Index", "Home");
            }

            var model = new BannerEditModel
            {
                Name = bannerToDelete.Name,
                ValidFrom = bannerToDelete.ValidFrom,
                ValidUntil = bannerToDelete.ValidUntil,
                ImageData = bannerToDelete.ImageData
            };

            return this.View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var bannerToDelete = this.db.Banners
                .FirstOrDefault(b => b.Id == id);

            if (bannerToDelete == null)
            {
                this.AddNotification("Can not load this banner", NotificationType.ERROR);
                return this.RedirectToAction("Index", "Home");
            }

            this.db.Banners.Remove(bannerToDelete);
            this.db.SaveChanges();
            this.AddNotification("Banner successfuly deleted", NotificationType.SUCCESS);

            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult Active(int? page)
        {
            var activeBanners = this.db.Banners
                 .Where(b => b.ValidUntil > DateTime.Now)
                 .OrderBy(x => Guid.NewGuid())
                 .Select(ActiveBannerViewModel.ViewModel)
                 .ToPagedList(page ?? GlobalConstants.DefaultPageNumber, GlobalConstants.DefaultPageSize);

            return this.View(activeBanners);
        }
    }
}
