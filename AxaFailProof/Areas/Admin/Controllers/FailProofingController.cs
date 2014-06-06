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
    public class FailProofingController : Controller
    {
        private AxaFailProofContext db = new AxaFailProofContext();

        //
        // GET: /Admin/FailProofing/

        public ViewResult Index()
        {
            return View(db.FailProofing.ToList());
        }

        //
        // GET: /Admin/FailProofing/Details/5

        public ViewResult Details(int id)
        {
            FailProofing failproofing = db.FailProofing.Find(id);
            return View(failproofing);
        }

        //
        // POST: /Admin/FailProofing/Delete/5

        //[HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            FailProofing failproofing = db.FailProofing.Find(id);
            db.FailProofing.Remove(failproofing);
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