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
    public class BannerController : Controller
    {
        private AxaFailProofContext db = new AxaFailProofContext();

        //
        // GET: /Admin/Banner/

        public ViewResult Index()
        {
            return View(db.Banners.Where(b => b.Position=="Home").OrderByDescending(b => b.DateCreated).ToList());
        }

        //
        // GET: /Admin/Banner/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Banner/Create
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Banner banner)
        {
            if (ModelState.IsValid)
            {
                banner.Position = "Home";
                banner.DateCreated = DateTime.Now;
                banner.Status = true;

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

                db.Banners.Add(banner);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(banner);
        }
        
        //
        // GET: /Admin/Banner/Edit/5
 
        public ActionResult Edit(int id)
        {
            Banner banner = db.Banners.Find(id);
            return View(banner);
        }

        //
        // POST: /Admin/Banner/Edit/5
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
        // POST: /Admin/Banner/Delete/5

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