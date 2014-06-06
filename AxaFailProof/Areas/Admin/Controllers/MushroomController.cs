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
    //[Authorize]
    public class MushroomController : Controller
    {
        private AxaFailProofContext db = new AxaFailProofContext();

        //
        // GET: /Admin/Mushroom/

        public ViewResult Index()
        {
            return View(db.Mushrooms.OrderByDescending(m=> m.DateCreated).ToList());
        }

        //
        // GET: /Admin/Mushroom/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Mushroom/Create
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Mushroom mushroom)
        {
            if (ModelState.IsValid)
            {
                mushroom.Status = true;
                mushroom.DateCreated = DateTime.Now;
                db.Mushrooms.Add(mushroom);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(mushroom);
        }
        
        //
        // GET: /Admin/Mushroom/Edit/5
 
        public ActionResult Edit(int id)
        {
            Mushroom mushroom = db.Mushrooms.Find(id);
            return View(mushroom);
        }

        //
        // POST: /Admin/Mushroom/Edit/5
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Mushroom mushroom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mushroom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mushroom);
        }

        //
        // POST: /Admin/Mushroom/Delete/5

        //[HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Mushroom mushroom = db.Mushrooms.Find(id);
            db.Mushrooms.Remove(mushroom);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Status(int id)
        {
            Mushroom mushroom = db.Mushrooms.Find(id);
            if (mushroom.Status == false)
            {
                mushroom.Status = true;
            }
            else
            {
                mushroom.Status = false;
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