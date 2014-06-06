using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AxaFailProof.Models;
using System.IO;
using System.Web.Helpers;

namespace AxaFailProof.Areas.Admin.Controllers
{
    [Authorize]
    public class BannerFooterController : Controller
    {
        private AxaFailProofContext db = new AxaFailProofContext();

        //
        // GET: /Admin/BannerFooter/

        public ViewResult Index()
        {
            return View(db.Banners.Where(b => b.Position == "Footer").OrderByDescending(b => b.DateCreated).ToList());
        }

        //
        // GET: /Admin/BannerFooter/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/BannerFooter/Create
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Banner banner)
        {
            if (ModelState.IsValid)
            {
                // save image process
                var image = WebImage.GetImageFromRequest();
                if (image != null)
                {
                    var filename = Path.GetFileName(image.FileName);
                    var guid = Guid.NewGuid().ToString().Substring(0, 8);
                    image.Save(Path.Combine("~/Userfiles/images", guid + "-" + filename));
                    //image.Resize(457, 385, true, true);
                    //image.Save(Path.Combine("~/Userfiles/image_thumb", guid + "-" + filename));
                    //image.Resize(398, 208, true, true);
                    //image.Save(Path.Combine("~/Userfiles/og_images", guid + "-" + filename));

                    banner.Image = Url.Content(guid + "-" + filename);
                }
                banner.Position = "Footer";
                banner.DateCreated = DateTime.Now;
                banner.Status = true;
                db.Banners.Add(banner);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(banner);
        }
        
        //
        // GET: /Admin/BannerFooter/Edit/5
 
        public ActionResult Edit(int id)
        {
            Banner banner = db.Banners.Find(id);
            return View(banner);
        }

        //
        // POST: /Admin/BannerFooter/Edit/5
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Banner banner)
        {
            if (ModelState.IsValid)
            {
                // save image process
                var image = WebImage.GetImageFromRequest();
                if (image != null)
                {
                    var filename = Path.GetFileName(image.FileName);
                    var guid = Guid.NewGuid().ToString().Substring(0, 8);
                    image.Save(Path.Combine("~/Userfiles/images", guid + "-" + filename));
                    //image.Resize(457, 385, true, true);
                    //image.Save(Path.Combine("~/Userfiles/image_thumb", guid + "-" + filename));
                    //image.Resize(398, 208, true, true);
                    //image.Save(Path.Combine("~/Userfiles/og_images", guid + "-" + filename));

                    banner.Image = Url.Content(guid + "-" + filename);
                }

                db.Entry(banner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banner);
        }

        //
        // POST: /Admin/BannerFooter/Delete/5

        //[HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Banner banner = db.Banners.Find(id);
            db.Banners.Remove(banner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Status(int id)
        {
            Banner banner = db.Banners.Find(id);
            if (banner.Status == false)
            {
                banner.Status = true;
            }
            else
            {
                banner.Status = false;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}