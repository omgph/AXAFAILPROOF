using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AxaFailProof.Models;
using System.Web.Helpers;
using System.IO;

namespace AxaFailProof.Areas.Admin.Controllers
{
    [Authorize]
    public class StoriesController : Controller
    {
        private AxaFailProofContext db = new AxaFailProofContext();

        //
        // GET: /Admin/Stories/

        public ViewResult Index()
        {
            var stories = db.Stories.OrderByDescending(s => s.DateCreated);
            return View(stories.ToList());
        }

        //
        // GET: /Admin/Stories/Create

        public ActionResult Create()
        {
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Title");
            return View();
        } 

        //
        // POST: /Admin/Stories/Create
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Story story)
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
                    image.Resize(457, 385, true, true);
                    image.Save(Path.Combine("~/Userfiles/image_thumb", guid + "-" + filename));
                    image.Resize(398, 208, true, true);
                    image.Save(Path.Combine("~/Userfiles/og_images", guid + "-" + filename));

                    story.Image = Url.Content(guid + "-" + filename);
                }

                story.DateCreated = DateTime.Now;
                story.Status = true;
                story.Featured = false;
                db.Stories.Add(story);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Title", story.TopicID);
            return View(story);
        }
        
        //
        // GET: /Admin/Stories/Edit/5
 
        public ActionResult Edit(int id)
        {
            Story story = db.Stories.Find(id);
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Title", story.TopicID);
            return View(story);
        }

        //
        // POST: /Admin/Stories/Edit/5
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Story story)
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
                    image.Resize(457, 385, true, true);
                    image.Save(Path.Combine("~/Userfiles/image_thumb", guid + "-" + filename));
                    image.Resize(398, 208, true, true);
                    image.Save(Path.Combine("~/Userfiles/og_images", guid + "-" + filename));

                    story.Image = Url.Content(guid + "-" + filename);
                }

                db.Entry(story).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Title", story.TopicID);
            return View(story);
        }

        //
        // POST: /Admin/Stories/Delete/5

        //[HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Story story = db.Stories.Find(id);
            db.Stories.Remove(story);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Status(int id)
        {
            Story story = db.Stories.Find(id);
            if (story.Status == false)
            {
                story.Status = true;
            }
            else
            {
                story.Status = false;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Featured(int id)
        {
            Story story = db.Stories.Find(id);
            if (story.Featured == false)
            {
                story.Featured = true;
            }
            else
            {
                story.Featured = false;
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