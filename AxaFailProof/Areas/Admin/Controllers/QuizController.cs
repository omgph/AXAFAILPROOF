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
    public class QuizController : Controller
    {
        private AxaFailProofContext db = new AxaFailProofContext();

        //
        // GET: /Admin/Quiz/

        public ViewResult Index()
        {
            return View(db.Quiz.ToList());
        }

        //
        // POST: /Admin/Quiz/Delete/5
        //[HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Quiz quiz = db.Quiz.Find(id);
            db.Quiz.Remove(quiz);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Status(int id)
        {
            Quiz quiz = db.Quiz.Find(id);
            if (quiz.Status == false)
            {
                quiz.Status = true;
            }
            else
            {
                quiz.Status = false;
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