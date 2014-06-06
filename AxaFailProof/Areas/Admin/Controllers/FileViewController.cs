using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AxaFailProof.ViewModel;
using AxaFailProof.Models;
using System.Web.Helpers;
using System.Configuration;
using System.IO;

namespace AxaFailProof.Areas.Admin.Controllers
{
    [Authorize]
    public class FileViewController : Controller
    {
        //
        // GET: /Admin/FileView/

        public ActionResult Index()
        {
            FileSystemView model = new FileSystemView()
            {
                TempltesImageFiles = GetImageList()
            };
            return View(model);
        }

        public static IList<FileItemView> GetImageList()
        {
            string path = ConfigurationManager.AppSettings["TemplateImagesPath"];
            IList<FileItemView> images = new List<FileItemView>();
            GetFiles(path, images);
            return images.OrderBy(img => img.FileName).ToList();
        }

        public static void GetFiles(string path, IList<FileItemView> images)
        {
            // get directory info folder path string
            DirectoryInfo dir = new DirectoryInfo(path);

            // loop all files from folder
            foreach (var image in dir.GetFiles("*.jpg"))
            {
                // add file
                images.Add(new FileItemView() { FileName = image.Name });
            }//end foreach (var image in dir.GetFiles("*.jpg"))

            // loop all files from folder
            foreach (var image in dir.GetFiles("*.png"))
            {
                // add file
                images.Add(new FileItemView() { FileName = image.Name });
            }//end foreach (var image in dir.GetFiles("*.png"))

            // loop all files from folder
            foreach (var image in dir.GetFiles("*.gif"))
            {
                // add file
                images.Add(new FileItemView() { FileName = image.Name });
            }//end foreach (var image in dir.GetFiles("*.gif"))

        }//end public static void GetFiles(string path, IList<FileItemView> images)

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var filename = Path.GetFileName(file.FileName);
                string[] validexts = { ".jpg", ".png", ".gif", ".JPG", ".PNG", ".GIF" };
                string ext = Path.GetExtension(file.FileName);
                if (validexts.Contains(ext))
                {
                    var path = Path.Combine(Server.MapPath("~/UserFiles/images"), filename);
                    file.SaveAs(path);
                    TempData["message"] = "You have successfully uploaded a file as " + Request.Url.Host.ToString() + "/UserFiles/images/" + filename;
                }
            }
            return RedirectToAction("Index");
        }
    }
}
