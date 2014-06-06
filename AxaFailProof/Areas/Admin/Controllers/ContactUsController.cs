using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AxaFailProof.Models;
using AxaFailProof.ExcelUtilities;
using System.IO;

namespace AxaFailProof.Areas.Admin.Controllers
{
    [Authorize]
    public class ContactUsController : Controller
    {
        private AxaFailProofContext db = new AxaFailProofContext();

        //
        // GET: /Admin/ContactUs/

        public ViewResult Index()
        {
            return View(db.ContactUs.ToList());
        }

        //
        // GET: /Admin/ContactUs/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/ContactUs/Create

        [HttpPost]
        public ActionResult Create(ContactU contactu)
        {
            if (ModelState.IsValid)
            {
                contactu.DateCreated = DateTime.Now;
                db.ContactUs.Add(contactu);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(contactu);
        }
        
        //
        // GET: /Admin/ContactUs/Edit/5
 
        public ActionResult Edit(int id)
        {
            ContactU contactu = db.ContactUs.Find(id);
            return View(contactu);
        }

        //
        // POST: /Admin/ContactUs/Edit/5

        [HttpPost]
        public ActionResult Edit(ContactU contactu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactu);
        }

        //
        // POST: /Admin/ContactUs/Delete/5

        //[HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ContactU contactu = db.ContactUs.Find(id);
            db.ContactUs.Remove(contactu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void DownloadExl()
        {
            var newsletter = new DataTable();
            newsletter.Columns.Add(new DataColumn("Name", Type.GetType("System.String")));
            newsletter.Columns.Add(new DataColumn("Email", Type.GetType("System.String")));
            newsletter.Columns.Add(new DataColumn("Message", Type.GetType("System.String")));
            newsletter.Columns.Add(new DataColumn("DateCreated", Type.GetType("System.String")));

            var registerduser = db.ContactUs.OrderByDescending(n => n.DateCreated);
            foreach (var item in registerduser)
            {
                object[] data = new Object[4];
                data[0] = item.Name;
                data[1] = item.Email;
                data[2] = item.Message;
                data[3] = item.DateCreated.ToString("yyyy-MM-dd");
                newsletter.Rows.Add(data);
            }

            MemoryStream stream = ExcelUtility.GetExcel(newsletter);

            var filename = DateTime.Now + "_ContactUs.xlsx";
            var contenttype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Clear();
            Response.ContentType = contenttype;
            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(stream.ToArray());
            Response.End();
        }            


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}