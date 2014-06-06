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
    public class SocialMediaUserController : Controller
    {
        private AxaFailProofContext db = new AxaFailProofContext();

        //
        // GET: /Admin/SocialMediaUser/

        public ViewResult Index()
        {
            return View(db.SocialUsers.ToList());
        }

        public void DownloadExl()
        {
            var newsletter = new DataTable();
            newsletter.Columns.Add(new DataColumn("SocialMedia", Type.GetType("System.String")));
            newsletter.Columns.Add(new DataColumn("Name", Type.GetType("System.String")));
            newsletter.Columns.Add(new DataColumn("Email", Type.GetType("System.String")));
            newsletter.Columns.Add(new DataColumn("DateCreated", Type.GetType("System.String")));

            var mediauser = db.SocialUsers.OrderByDescending(n => n.DateCreated);
            foreach (var item in mediauser)
            {
                object[] data = new Object[4];
                data[0] = item.SocialMedia;
                data[1] = item.FullName;
                data[2] = item.Email;
                data[3] = item.DateCreated;
                newsletter.Rows.Add(data);
            }

            MemoryStream stream = ExcelUtility.GetExcel(newsletter);

            var filename = DateTime.Now + "_SocialMedia.xlsx";
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