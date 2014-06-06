using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AxaFailProof.Models;

namespace AxaFailProof.Areas.Admin.Controllers
{
    [Authorize]
    public class StaticPageController : Controller
    {
        private AxaFailProofContext db = new AxaFailProofContext();

        //
        // GET: /Admin/StaticPage/

        public ViewResult Index()
        {
            return View(db.StaticPages.OrderByDescending(s => s.DateCreated).ToList());
        }

        //
        // GET: /Admin/StaticPage/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/StaticPage/Create
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(StaticPage staticpage)
        {
            if (ModelState.IsValid)
            {
                staticpage.Status = true;
                staticpage.DateCreated = DateTime.Now;
                db.StaticPages.Add(staticpage);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(staticpage);
        }
        
        //
        // GET: /Admin/StaticPage/Edit/5
 
        public ActionResult Edit(int id)
        {
            StaticPage staticpage = db.StaticPages.Find(id);
            return View(staticpage);
        }

        //
        // POST: /Admin/StaticPage/Edit/5
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(StaticPage staticpage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staticpage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staticpage);
        }

        //
        // POST: /Admin/StaticPage/Delete/5

        //[HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            StaticPage staticpage = db.StaticPages.Find(id);
            db.StaticPages.Remove(staticpage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Status(int id)
        {
            StaticPage staticpage = db.StaticPages.Find(id);
            if (staticpage.Status== false)
            {
                staticpage.Status = true;
            }
            else
            {
                staticpage.Status = false;
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