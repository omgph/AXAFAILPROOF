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
    public class HealthRiskScoreController : Controller
    {
        private AxaFailProofContext db = new AxaFailProofContext();

        //
        // GET: /Admin/HealthRiskScore/

        public ViewResult Index()
        {
            return View(db.HealthRiskScore.ToList());
        }

        //
        // POST: /Admin/HealthRiskScore/Delete/5

        //[HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            HealthRiskScore healthriskscore = db.HealthRiskScore.Find(id);
            db.HealthRiskScore.Remove(healthriskscore);
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