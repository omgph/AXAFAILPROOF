using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Helpers;
using System.Net.Mail;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Net;
using AxaFailProof.Models;
using System.Web.Helpers;
using System.Data;
using PagedList;
using Utils;

namespace AxaFailProof.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private AxaFailProofContext db = new AxaFailProofContext();
        private string ogimagepath = ConfigurationManager.AppSettings["TemplateOgImageHost"];

        // Home
        public ActionResult Index()
        {
            AxaViewModel model = new AxaViewModel();
            model.FotterBanner = db.Banners.Where(b => b.Position == "Footer" && b.Status == true).OrderByDescending(b => b.DateCreated).Take(1);
            model.Mushroom1 = db.Mushrooms.Find(4);
            model.Mushroom2 = db.Mushrooms.Find(2);
            model.FeaturedStory = db.Stories.Where(s => s.Status == true && s.Featured == true).Take(1);
            model.HealthFacts = db.Stories.Where(s => s.Status == true && s.TopicID == 11).OrderByDescending(s => s.DateCreated).Take(1);
            return View(model);
        }

        // FailProofYourHealth
        public ActionResult FailProofYourHealth(int id) 
        {
            AxaViewModel model = new AxaViewModel();

            model.StaticPage = db.StaticPages.Find(id);
            ViewBag.MetaTitle = model.StaticPage.Title;
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";
            if (model.StaticPage.MetaKeyWords != null)
            {
                ViewBag.MetaKeyWords = model.StaticPage.MetaKeyWords + ",AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            }
            else {
                ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            }
            if (model.StaticPage.MetaDescription != null){
                ViewBag.MetaDescription = model.StaticPage.MetaDescription;
            }
            else {
                ViewBag.MetaDescription = "";
            }
            return View(model);
        }

        public ActionResult RegisterEvent()
        {
            ViewBag.MetaTitle = "Join a Pocket Event";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            AxaViewModel model = new AxaViewModel();
            model.Locations = db.Locations.Where(l => l.Status == true).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterEvent(EventsRegistered eventsregistered)
        {
            ViewBag.MetaTitle = "Join a Pocket Event";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            if (ModelState.IsValid)
            {
                eventsregistered.DateCreated = DateTime.Now;
                eventsregistered.Status = true;
                db.EventsRegistereds.Add(eventsregistered);
                db.SaveChanges();

                return RedirectToAction("RegisterEventStep3", new { id = eventsregistered.RegEventID });
            }
            return View();
        }

        [HttpPost]
        public string EventCheck(int locationid)
        {
            var eventcheck = db.EventsRegistereds.Where(e => e.LocationID == locationid).Count();
            return eventcheck.ToString();
        }

        [HttpPost]
        public string EventDetail(int locationid)
        {
            var location = "";
            var locationcheck = db.Locations.Find(locationid);
            location = "<h11><h9>Title:</h9> " + locationcheck.Event + "<br ><h9>Description:</h9> " + locationcheck.EventDescription + "<br ><h9>Date:</h9> " + locationcheck.EventDate + "<br ><h9>Time:</h9> " + locationcheck.EventTime + "<br ><h9>Location:</h9> " + locationcheck.Location1 + "<br ></h11>";
            return location;
        }

        public ActionResult RegisterEventStep3(int id)
        {
            ViewBag.MetaTitle = "Join a Pocket Event";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            AxaViewModel model = new AxaViewModel();
            model.EventsRegistered = db.EventsRegistereds.Find(id);
            model.Locations = db.Locations.Where(l => l.Status == true).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterEventStep3(int id, EventsRegistered eventsregistered)
        {
            ViewBag.MetaTitle = "Join a Pocket Event";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            if (!ReCaptcha.Validate(privateKey: "6Ldg9N0SAAAAAKcadBDB7nP7vH0iBprFdJgRmZ5I"))
            {
                return new RedirectResult(Url.Action("RegisterEventStep3", new { id = id }) + "#c");
            }
            if (eventsregistered.Terms == true)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(eventsregistered).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("RegisterThankyou");
                }
            }

            AxaViewModel model = new AxaViewModel();
            model.EventsRegistered = db.EventsRegistereds.Find(id);
            model.Locations = db.Locations.Where(l => l.Status == true).ToList();
            return View(model);
        }

        public ActionResult RegisterThankyou() 
        {
            ViewBag.MetaTitle = "Thank you";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";
            return View();
        }

        // WhatIsFailProof
        public ActionResult WhatIsFailProof(int id)
        {
            AxaViewModel model = new AxaViewModel();

            model.StaticPage = db.StaticPages.Find(id);
            ViewBag.MetaTitle = model.StaticPage.Title;
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";
            if (model.StaticPage.MetaKeyWords != null)
            {
                ViewBag.MetaKeyWords = model.StaticPage.MetaKeyWords + ",AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            }
            else
            {
                ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            }
            if (model.StaticPage.MetaDescription != null)
            {
                ViewBag.MetaDescription = model.StaticPage.MetaDescription;
            }
            else
            {
                ViewBag.MetaDescription = "";
            }
            return View(model);
        }

        // Stories
        public ActionResult Archive(string month, string year)
        {
            ViewBag.MetaTitle = "Archive";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            AxaViewModel model = new AxaViewModel();
            model.Stories = db.Stories.Where(s => s.Status == true && s.TopicID > 0 && s.TopicID != 11).OrderByDescending(s => s.DateCreated).ToList();
            return View(model);
        }

        public ActionResult ArchiveByDate(int month, int year)
        {
            ViewBag.MetaTitle = "Archive";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            AxaViewModel model = new AxaViewModel();
            model.Stories = db.Stories.Where(s => s.Status == true && s.TopicID > 0 && s.TopicID != 11 && s.DateCreated.Value.Month == month && s.DateCreated.Value.Year == year).OrderByDescending(s => s.DateCreated).ToList();
            return View(model);
        }


        public ActionResult Story(int id, string title)
        {
            AxaViewModel model = new AxaViewModel();
            model.StoryID = id;
            model.Story = db.Stories.Find(id);
            ViewBag.MetaTitle = model.Story.Title;
            ViewBag.OgImage = ogimagepath + model.Story.Image;
            if (model.Story.MetaKeyWords != null)
            {
                ViewBag.MetaKeyWords = model.Story.MetaKeyWords + ",AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            }
            else
            {
                ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            }
            if (model.Story.MetaDescription != null)
            {
                ViewBag.MetaDescription = model.Story.MetaDescription;
            }
            else
            {
                ViewBag.MetaDescription = "";
            }

            model.Comments = db.Comments.Where(c => c.StoryID == id && c.Status == true).OrderByDescending(s => s.DateCreated).ToList();

            // count total comments
            var TotalComments = db.Comments.Where(c => c.StoryID == id && c.Status == true).OrderByDescending(s => s.DateCreated).Count();
            Story story = db.Stories.Find(id);
            story.CommnetsCount = TotalComments;
            // save total count
            db.Entry(story).State = EntityState.Modified;
            db.SaveChanges();

            return View(model);
        }

        // MAKE AJAX POST
        [HttpPost]
        public string CommentForm(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.DateCreated = DateTime.Now;
                comment.Status = false;
                db.Comments.Add(comment);
                db.SaveChanges();
                return "ok";
            }
            return "false";
        }

        public ActionResult CommentsThankyou()
        {
            ViewBag.MetaTitle = "Thank you";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";
            return View();
        }

        public ActionResult SubmitStoryLogin()
        {
            ViewBag.MetaTitle = "Share your Story";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";
            return View();
        }


        public ActionResult SubmitStory()
        {
            ViewBag.MetaTitle = "Write your own story";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";
            return View();
        }

        [HttpPost]
        public ActionResult SubmitStory(Story story)
        {
            ViewBag.MetaTitle = "Write your own story";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

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
                story.Status = false;
                story.Featured = false;
                story.CommnetsCount = 0;
                db.Stories.Add(story);
                db.SaveChanges();
                return RedirectToAction("StoryThankyou");
            }
            return View();
        }

        public ActionResult StoryThankyou()
        {
            ViewBag.MetaTitle = "Thank you";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";
            return View();
        }

        public ActionResult Search(int? page)
        {
            var pageNumber = page ?? 1;
            int pageSize = 3;

            ViewBag.MetaTitle = "Search by Topic";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            AxaViewModel model = new AxaViewModel();
            var count = db.Stories.Where(s => s.Status == true && s.TopicID > 0 && s.TopicID != 11).Count();
            if (count == 0)
            {
                return RedirectToAction("NoRecordFound");
            }
            model.SearchResults = db.Stories.Where(s => s.Status == true && s.TopicID > 0 && s.TopicID != 11).OrderByDescending(s => s.DateCreated).ToPagedList(pageNumber, pageSize);
            return View(model);
        }


        public ActionResult SearchTopic(int id, string title, int? page)
        {
            var pageNumber = page ?? 1;
            int pageSize = 3;

            ViewBag.MetaTitle = "Search by Topic";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            AxaViewModel model = new AxaViewModel();
            var count = db.Stories.Where(s => s.Status == true && s.TopicID == id).Count();
            if (count == 0)
            {
                return RedirectToAction("NoRecordFound");
            }
            model.SearchResults = db.Stories.Where(s => s.Status == true && s.TopicID == id).OrderByDescending(s => s.DateCreated).ToPagedList(pageNumber, pageSize);
            return View(model);
        }

        [HttpPost]
        public ActionResult SearchTags(string SearchTags, int? page)
        {
            var pageNumber = page ?? 1;
            int pageSize = 3;

            ViewBag.MetaTitle = "Search by Tags";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            AxaViewModel model = new AxaViewModel();
            var count = db.Stories.Where(s => s.Status == true && s.Tags.Contains(SearchTags)).Count();
            if (count == 0) {
                return RedirectToAction("NoRecordFound");
            }
            model.SearchResults = db.Stories.Where(s => s.Status == true && s.Tags.Contains(SearchTags)).OrderByDescending(s => s.DateCreated).ToPagedList(pageNumber, pageSize);
            return View(model);
        }

        public ActionResult NoRecordFound()
        {
            ViewBag.MetaTitle = "No Record Found";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            return View();
        }

        // Contact Us
        public ActionResult ContactUs()
        {
            ViewBag.MetaTitle = "Contact Us";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";
            AxaViewModel model = new AxaViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ContactUs(ContactU contactus)
        {
            ViewBag.MetaTitle = "Contact Us";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            if (ModelState.IsValid)
            {
                contactus.DateCreated = DateTime.Now;
                db.ContactUs.Add(contactus);
                db.SaveChanges();
                SendMail(contactus);
                return RedirectToAction("ContactUsThankyou");
            }

            return View();
        }

        public ActionResult ContactUsThankyou()
        {
            ViewBag.MetaTitle = "Thank you";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";
            return View();
        }

        public ActionResult GoogleSearch()
        {
            ViewBag.MetaTitle = "Search";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";
            return View();
        }

        public ActionResult HealthFactsFigures(int? page)
        {
            var pageNumber = page ?? 1;
            int pageSize = 3;

            ViewBag.MetaTitle = "Health Facts & Figures";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            AxaViewModel model = new AxaViewModel();
            var count = db.Stories.Where(s => s.Status == true && s.TopicID == 11).Count();
            if (count == 0)
            {
                return RedirectToAction("NoRecordFound");
            }


            model.SearchResults = db.Stories.Where(s => s.Status == true && s.TopicID == 11).OrderByDescending(s => s.DateCreated).ToPagedList(pageNumber, pageSize);
            return View(model);
        }

        public ActionResult HealthDetail(int id, string title)
        {
            AxaViewModel model = new AxaViewModel();
            model.StoryID = id;
            model.Story = db.Stories.Find(id);
            ViewBag.MetaTitle = model.Story.Title;
            ViewBag.OgImage = ogimagepath + model.Story.Image;
            if (model.Story.MetaKeyWords != null)
            {
                ViewBag.MetaKeyWords = model.Story.MetaKeyWords + ",AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            }
            else
            {
                ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            }
            if (model.Story.MetaDescription != null)
            {
                ViewBag.MetaDescription = model.Story.MetaDescription;
            }
            else
            {
                ViewBag.MetaDescription = "";
            }

            model.Comments = db.Comments.Where(c => c.StoryID == id && c.Status == true).OrderByDescending(s => s.DateCreated).ToList();

            // count total comments
            var TotalComments = db.Comments.Where(c => c.StoryID == id && c.Status == true).OrderByDescending(s => s.DateCreated).Count();
            Story story = db.Stories.Find(id);
            story.CommnetsCount = TotalComments;
            // save total count
            db.Entry(story).State = EntityState.Modified;
            db.SaveChanges();

            return View(model);
        }

        private void SendMail(ContactU contactus)
        {
            MailMessage message = new MailMessage();
            message.Subject = "AXA Inquiry";
            string emailto = ConfigurationManager.AppSettings["emailto"];
            string emailcc = ConfigurationManager.AppSettings["emailcc"];
            message.From = new MailAddress(contactus.Email);
            message.To.Add(new MailAddress(emailto));
            message.CC.Add(new MailAddress(emailcc));
            message.Body = "Name : " + contactus.Name + "<br />";
            message.Body += "Email Address: " + contactus.Email + "<br />";
            message.Body += "Message: " + contactus.Message + "<br />";
            message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            try
            {
                smtp.Send(message);
            }
            catch (Exception e) { }
        }

        public ActionResult NotFound()
        {
            ViewBag.MetaTitle = "Page not found";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";
            return View();
        }

        public ActionResult Quiz()
        {
            ViewBag.MetaTitle = "Offbeat Death Quiz";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            AxaViewModel model = new AxaViewModel();
            model.Mushroom4 = db.Mushrooms.Find(6);
            model.Question1 = db.Mushrooms.Find(7);
            model.Question2 = db.Mushrooms.Find(8);
            model.Question3 = db.Mushrooms.Find(9);
            model.Question4 = db.Mushrooms.Find(10);
            model.Question5 = db.Mushrooms.Find(11);
            model.Question6 = db.Mushrooms.Find(12);
            return View(model);
        }

        [HttpPost]
        public ActionResult Quiz(Quiz quiz)
        {
            ViewBag.MetaTitle = "Offbeat Death Quiz";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            if (ModelState.IsValid)
            {
                quiz.DateCreated = DateTime.Now;
                quiz.Status = true;
                db.Quiz.Add(quiz);
                db.SaveChanges();

                return RedirectToAction("QuizRegistration", new { id = quiz.QuizID });
            }
            AxaViewModel model = new AxaViewModel();
            model.Mushroom4 = db.Mushrooms.Find(6);
            return View(model);
        }

        public ActionResult QuizRegistration(int id) 
        {
            ViewBag.MetaTitle = "Registration";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            AxaViewModel model = new AxaViewModel();
            model.Quiz = db.Quiz.Find(id);
            return View(model);
        }

        public string SaveSocialMedia(string userid, string name, string email, string mediatype, SocialUser socialuser)
        {
            var usercount = db.SocialUsers.Where( s => s.SocialID == userid).Count();
            if (usercount == 0)
            {
                socialuser.SocialID = userid;
                socialuser.FullName = name;
                socialuser.Email = email;
                socialuser.SocialMedia = mediatype;
                socialuser.DateCreated = DateTime.Now;
                db.SocialUsers.Add(socialuser);
                db.SaveChanges();
                return "Save";
            }
            else {
                return "Exist";
            }
        }

        [HttpPost]
        public ActionResult QuizRegistration(int id, Quiz quiz)
        {
            ViewBag.MetaTitle = "Registration";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            if (ModelState.IsValid)
            {
                db.Entry(quiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Results", new {id = id});
            }

            AxaViewModel model = new AxaViewModel();
            model.Quiz = db.Quiz.Find(id);
            return View(model);            
        }

        public ActionResult Results(int id)
        {
            ViewBag.MetaTitle = "Result";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            AxaViewModel model = new AxaViewModel();
            model.Quiz = db.Quiz.Find(id);
            if (Convert.ToInt32(model.Quiz.Score) >= 8)
            {
                ViewBag.ResultsTitle = "Death by paper cut";
                ViewBag.ResultsDescription = "Your boss asks you to accomplish a mountain of paper work. While sorting the printed copies alone in the office late one night, a crisp A4 sized sheet of paper gives your thumb a papercut.You bleed uncontrollably. You are too week to reach for the phone to call the paramedics. You are found on the floor the next morning, lifeless, surrounded by a pool of your own blood.";
                ViewBag.ImageResults = "02-result-paper-cut.png";
            }
            if (Convert.ToInt32(model.Quiz.Score) >= 11)
            {
                ViewBag.ResultsTitle = "Die as an alien sacrifice";
                ViewBag.ResultsDescription = "While watching TV alone at home one night, you hear a faint knock on the door. You look through the peephole but see nothing but an empty lawn. As you start to head back to the living room, you hear the doorbell again. You hesitantly turn around and open the front door. A blinding light beams your spirit up to Zultan, the alien king of Planet Zhor. Your body is left lifeless on your front porch.";
                ViewBag.ImageResults = "03-result-alien-sacrifice.png";
            }
            if (Convert.ToInt32(model.Quiz.Score) >= 14)
            {
                ViewBag.ResultsTitle = "Death in the hands of Chuck Norris";
                ViewBag.ResultsDescription = "You manage to piss off Chuck Norris by making fun of his chest hair. Unfortunately, your self defense skills are no match to  his killer roundhouse.";
                ViewBag.ImageResults = "10-result-chucknorris.png";
            }
            if (Convert.ToInt32(model.Quiz.Score) >= 17)
            {
                ViewBag.ResultsTitle = "A horde of buckethead zombies invade your house and eat your brains";
                ViewBag.ResultsDescription = "You procrastinate all weekend and don't manage to tend to your garden. One eerily quiet Sunday afternoon, you hear the front door of your house creak open, and a deep, throaty voice groans,  BRAAAIIIINSSS Before you know it, you are surrounded by a horde of buckethead zombies, and the potted house plant you desperately clutch to is devoured in seconds, along with your brains.";
                ViewBag.ImageResults = "06-result-horde-zombies.png";
            }
            if (Convert.ToInt32(model.Quiz.Score) >= 20)
            {
                ViewBag.ResultsTitle = "Death by elephant stampede";
                ViewBag.ResultsDescription = "You go on an African safari. While riding through a vast patch of land somewhere in Kenya, your vehicle conks out, so the driver has no choice but to pop the hood and figure out how to get the engine running again. You step out of the vehicle to stretch your legs. You feel your stomach grumble, when remember that you saved the airline peanuts they distributed on the flight to Kenya. You reach inside your pocket to fish out the tiny foil packet. The sound of foil ripping echoes through the desert, and a few seconds later, you are trampled to death by a herd of elephants fighting over the peanuts in your hand.";
                ViewBag.ImageResults = "05-result-elephant-stampede.png";
            }
            if (Convert.ToInt32(model.Quiz.Score) >= 23)
            {
                ViewBag.ResultsTitle = "Death by hotdog eating contest";
                ViewBag.ResultsDescription = "At the town fair, you join a hotdog eating contest, in the attempt to win a weekend trip to Bangladesh. You are on your 64th hotdog when a stray piece of processed meat accidentally gets lodged in your trachea, cutting off oxygen from your lungs. You wave your hands frantically to ask for help, but the crowd mistakes it for extreme enthusiasm. You eventually pass out and no one is able to revive you.";
                ViewBag.ImageResults = "07-result-hotdog-contest.png";
            }
            if (Convert.ToInt32(model.Quiz.Score) >= 26)
            {
                ViewBag.ResultsTitle = "Death by poisonous apple";
                ViewBag.ResultsDescription = "A vain old woman has a magic mirror hidden in her dark, eerie castle. She asks he mirror on the wall, who is the fairest one of all? Your face appears in the mirror. The old woman is furious. She is so enraged that she locates your house, tricks you into taking a bite out of a poisonous apple, and leaves your lifeless body strewn across the floor while she bellows in evil laughter all the way back to her castle.";
                ViewBag.ImageResults = "08-result-poisonous-apple.png";
            }
            if (Convert.ToInt32(model.Quiz.Score) >= 29)
            {
                ViewBag.ResultsTitle = "Death by selfie";
                ViewBag.ResultsDescription = "You love selfies, and you love taking them against historical landmarks. While giving peace signs. While exploring a heritage district up north, you stumble upon a 16th century church, with a façade that took your breath away. You take out your iPhone, and prepare to take a selfie with the church visible in the background, when a speeding car suddently turns the corner at full speed and runs you over.";
                ViewBag.ImageResults = "09-result-selfie.png";
            }
            if (Convert.ToInt32(model.Quiz.Score) >= 32)
            {
                ViewBag.ResultsTitle = "Death by Candy Crush";
                ViewBag.ResultsDescription = "You cannot get past Level 231. You spend every waking minute on your smart phone, trying to beat the timer on those pesky bombs. You are so consumed by the game, that you don't eat, sleep, or bathe. You become severely malnourished and dehydrated, and eventually die of organ failure.";
                ViewBag.ImageResults = "04-result-candy-crush.png";
            }
            if (Convert.ToInt32(model.Quiz.Score) >= 35)
            {
                ViewBag.ResultsTitle = "Death by piranha attack";
                ViewBag.ResultsDescription = "You and a friend decide to explore the Amazon. While enjoying a boat ride along the Orinoco River, your friend invites you to take a dip. You think it's a fabulous idea. You jump in the river simultaneously, and in the process, give about 80 piranhas their dinner for the day.";
                ViewBag.ImageResults = "09-result-piranha-attack.png";
            }

            return View(model);
        }

        public ActionResult RegFailProofing()
        {
            ViewBag.MetaTitle = "Registration";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";
            return View();
        }

        [HttpPost]
        public ActionResult RegFailProofing(FailProofing failproofing)
        {
            ViewBag.MetaTitle = "Registration";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            if (ModelState.IsValid)
            {
                failproofing.DateCreated = DateTime.Now;
                failproofing.Status = true;
                db.FailProofing.Add(failproofing);
                db.SaveChanges();
                return RedirectToAction("FailProofingThankyou");
            }
            return View();
        }

        public ActionResult FailProofingThankyou()
        {
            ViewBag.MetaTitle = "Thank you";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";
            return View();
        }

        public ActionResult HealthRiskScore()
        {
            ViewBag.MetaTitle = "Health Risk Score";
            ViewBag.MetaKeyWords = "AXA Philippines,Life Insurance,AXA Health Solutions,It Can Happen To You,Health MaX,Health eXentials,Health Insurance,Health Plan,Health Benefits,Best Health Plan,Global Insurance Company,Critical Illness Cover,Daily Hospitalization Income,All In One Plan,All In One Health Plan,Additional Health Coverage,Additional HMO Coverage,Breast Cancer,Cancer,Lung Cancer,Cervical Cancer,Symptoms of Cancer,Health Illness,Stroke Symptoms,Heart Stroke,What Is Stroke,Cancer Chemotherapy,Symptoms Tuberculosis,Pulmonary Tuberculosis,Health Care,Philippine Health Insurance,Health Card";
            ViewBag.MetaDescription = "";
            ViewBag.OgImage = ogimagepath + "axa_og_image.png";

            return View();
        }

        public string SaveHealthRiskScore(int income, int savings, string savefor, string owned, string bday, string gender, string smoker, int pounds, int feet, int inches, string parentsillness, string youillness, int alcohol, int coffee, string exercise, string emailadd, HealthRiskScore healthriskscore)
        {
            healthriskscore.ID = 1;
            healthriskscore.Income = income;
            healthriskscore.Savings = savings;
            healthriskscore.SaveFor = savefor;
            healthriskscore.Owned = owned;
            healthriskscore.Bday = bday;
            healthriskscore.Gender = gender;
            healthriskscore.Smoker = smoker;
            healthriskscore.Pounds = pounds;
            healthriskscore.Feet = feet;
            healthriskscore.Inches = inches;
            healthriskscore.ParentsIllness = parentsillness;
            healthriskscore.YouIllness = youillness;
            healthriskscore.Alcohol = alcohol;
            healthriskscore.Coffee = coffee;
            healthriskscore.Exercise = exercise;
            healthriskscore.Email = emailadd;

            db.HealthRiskScore.Add(healthriskscore);
            db.SaveChanges();
            return "Save";
        }


        [ChildActionOnly]
        public ActionResult Navigator() 
        {
            AxaViewModel model = new AxaViewModel();

            string _Action = ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            var _Id = ControllerContext.ParentActionViewContext.RouteData.Values["id"];
            switch (_Action)
            { 
                case "Index":
                    model.Banner = db.Banners.Where(b => b.Position == "Home" && b.Status == true).OrderByDescending(b => b.DateCreated).ToList();
                    ViewBag.Hselected = "current-menu-item";
                    break;
                case "RegFailProofing":
                    model.Banner = db.Banners.Where(b => b.Position == "Home" && b.Status == true).OrderByDescending(b => b.DateCreated).ToList();
                    ViewBag.Hselected = "current-menu-item";
                    break;
                case "FailProofingThankyou":
                    model.Banner = db.Banners.Where(b => b.Position == "Home" && b.Status == true).OrderByDescending(b => b.DateCreated).ToList();
                    ViewBag.Hselected = "current-menu-item";
                    break;
                case "GoogleSearch":
                    model.Banner = db.Banners.Where(b => b.Position == "Home" && b.Status == true).Take(1);
                    ViewBag.Hselected = "current-menu-item";
                    break;
                case "NotFound":
                    model.Banner = db.Banners.Where(b => b.Position == "Home" && b.Status == true).Take(1);
                    ViewBag.Hselected = "current-menu-item";
                    break;
                case "FailProofYourHealth":
                    if (_Id.ToString() == "8" || _Id.ToString() == "10" || _Id.ToString() == "11" || _Id.ToString() == "15" || _Id.ToString() == "16" || _Id.ToString() == "17")
                    {
                        model.Banner = db.Banners.Where(b => b.Position == "Exentials" && b.Status == true).Take(1);
                        ViewBag.Fselected = "current-menu-item";
                    }
                    else if (_Id.ToString() == "9" || _Id.ToString() == "12" || _Id.ToString() == "13" || _Id.ToString() == "14" || _Id.ToString() == "18")
                    {
                        model.Banner = db.Banners.Where(b => b.Position == "Max" && b.Status == true).Take(1);
                        ViewBag.Fselected = "current-menu-item";
                    }
                    else
                    {
                        model.Banner = db.Banners.Where(b => b.Position == "Fail Proof your Health" && b.Status == true).Take(1);
                        ViewBag.Fselected = "current-menu-item";
                    }
                    break;
                case "HealthFactsFigures":
                    model.Banner = db.Banners.Where(b => b.Position == "Fail Proof your Health" && b.Status == true).Take(1);
                    ViewBag.Fselected = "current-menu-item";
                    break;
                case "HealthDetail":
                    model.Banner = db.Banners.Where(b => b.Position == "Fail Proof your Health" && b.Status == true).Take(1);
                    ViewBag.Fselected = "current-menu-item";
                    break;
                case "WhatIsFailProof":
                    model.Banner = db.Banners.Where(b => b.Position == "What is Fail Proof" && b.Status == true).Take(1);
                    ViewBag.Wselected = "current-menu-item";
                    break;
                case "RegisterEvent":
                    model.Banner = db.Banners.Where(b => b.Position == "Event" && b.Status == true).Take(1);
                    ViewBag.Fselected = "current-menu-item";
                    break;
                case "RegisterEventStep3":
                    model.Banner = db.Banners.Where(b => b.Position == "Event" && b.Status == true).Take(1);
                    ViewBag.Fselected = "current-menu-item";
                    break;
                case "RegisterThankyou":
                    model.Banner = db.Banners.Where(b => b.Position == "Event" && b.Status == true).Take(1);
                    ViewBag.Fselected = "current-menu-item";
                    break;
                case "Archive":
                    model.Banner = db.Banners.Where(b => b.Position == "Stories" && b.Status == true).Take(1);
                    ViewBag.Sselected = "current-menu-item";
                    break;
                case "ArchiveByDate":
                    model.Banner = db.Banners.Where(b => b.Position == "Stories" && b.Status == true).Take(1);
                    ViewBag.Sselected = "current-menu-item";
                    break;
                case "Story":
                    model.Banner = db.Banners.Where(b => b.Position == "Stories" && b.Status == true).Take(1);
                    ViewBag.Sselected = "current-menu-item";
                    break;
                case "SubmitStory":
                    model.Banner = db.Banners.Where(b => b.Position == "Stories" && b.Status == true).Take(1);
                    ViewBag.Sselected = "current-menu-item";
                    break;
                case "SearchTopic":
                    model.Banner = db.Banners.Where(b => b.Position == "Stories" && b.Status == true).Take(1);
                    ViewBag.Sselected = "current-menu-item";
                    break;
                case "SearchTags":
                    model.Banner = db.Banners.Where(b => b.Position == "Stories" && b.Status == true).Take(1);
                    ViewBag.Sselected = "current-menu-item";
                    break;
                case "SubmitStoryLogin":
                    model.Banner = db.Banners.Where(b => b.Position == "Stories" && b.Status == true).Take(1);
                    ViewBag.Sselected = "current-menu-item";
                    break;
                case "StoryThankyou":
                    model.Banner = db.Banners.Where(b => b.Position == "Stories" && b.Status == true).Take(1);
                    ViewBag.Sselected = "current-menu-item";
                    break;
                case "Search":
                    model.Banner = db.Banners.Where(b => b.Position == "Stories" && b.Status == true).Take(1);
                    ViewBag.Sselected = "current-menu-item";
                    break;
                case "CommentsThankyou":
                    model.Banner = db.Banners.Where(b => b.Position == "Stories" && b.Status == true).Take(1);
                    ViewBag.Sselected = "current-menu-item";
                    break;
                case "NoRecordFound":
                    model.Banner = db.Banners.Where(b => b.Position == "Stories" && b.Status == true).Take(1);
                    ViewBag.Sselected = "current-menu-item";
                    break;
                case "ContactUs":
                    model.Banner = db.Banners.Where(b => b.Position == "Contact Us" && b.Status == true).Take(1);
                    ViewBag.Cselected = "current-menu-item";
                    break;
                case "ContactUsThankyou":
                    model.Banner = db.Banners.Where(b => b.Position == "Contact Us" && b.Status == true).Take(1);
                    ViewBag.Cselected = "current-menu-item";
                    break;
                case "Quiz":
                    model.Banner = db.Banners.Where(b => b.Position == "Quiz" && b.Status == true).Take(1);
                    ViewBag.Hselected = "current-menu-item";
                    break;
                case "QuizRegistration":
                    model.Banner = db.Banners.Where(b => b.Position == "Quiz" && b.Status == true).Take(1);
                    ViewBag.Hselected = "current-menu-item";
                    break;
                case "Results":
                    model.Banner = db.Banners.Where(b => b.Position == "Quiz" && b.Status == true).Take(1);
                    ViewBag.Hselected = "current-menu-item";
                    break;
                case "HealthRiskScore":
                    model.Banner = db.Banners.Where(b => b.Position == "HealthRiskScore" && b.Status == true).Take(1);
                    ViewBag.Hselected = "current-menu-item";
                    break;
            }

            return PartialView("_Navigator", model);
        }

        [ChildActionOnly]
        public ActionResult Navigator2()
        {
            return PartialView("_Navigator2");
        }

        [ChildActionOnly]
        public ActionResult FooterHome()
        {
            AxaViewModel model = new AxaViewModel();
            model.FotterBanner = db.Banners.Where(b => b.Position == "Footer" && b.Status == true).OrderByDescending(b => b.DateCreated).Take(1);
            model.Mushroom3 = db.Mushrooms.Find(5);
            return PartialView("_FooterHome", model);
        }

        [ChildActionOnly]
        public ActionResult FooterMain()
        {
            AxaViewModel model = new AxaViewModel();
            model.Mushroom3 = db.Mushrooms.Find(5);
            return PartialView("_FooterMain", model);
        }

        [ChildActionOnly]
        public ActionResult ResentPosts()
        {
            AxaViewModel model = new AxaViewModel();
            model.ResentPost = db.Stories.Where(s => s.Status == true).OrderByDescending(s => s.DateCreated).Take(8);
            return PartialView("_ResentPosts", model);
        }

        [ChildActionOnly]
        public ActionResult TopicList()
        {
            AxaViewModel model = new AxaViewModel();
            model.Topics = db.Topics.Where(t => t.Status == true && t.TopicID != 11).OrderBy(t => t.Title).ToList();
            return PartialView("_TopicList", model);
        }

        [ChildActionOnly]
        public ActionResult CopyRight()
        {
            return PartialView("_CopyRight");
        }

        [ChildActionOnly]
        public ActionResult ArchiveList()
        {
            AxaViewModel model = new AxaViewModel();
            var recordlist = db.Stories.Where(s => s.TopicID != 11).GroupBy(s => new { s.DateCreated.Value.Year, s.DateCreated.Value.Month}).Select(s => s.FirstOrDefault()).ToList();
            model.DateArchive = recordlist;
            return PartialView("_ArchiveList", model);
        }

        [ChildActionOnly]
        public ActionResult SearchByTopicForm()
        {
            return PartialView("_SearchByTopicForm");
        }


        // YAHOO LOGIN PART 
        public ActionResult YahooLogin()
        { 
            var endPoint = Utils.Yahoo.LogIn();
            return Redirect(endPoint);    
        }

        public ActionResult YahooCallBack(String oauth_token, String oauth_verifier)
        {
            AxaViewModel model = new AxaViewModel();
            model.Profile = Utils.Yahoo.GetUserData(oauth_token, oauth_verifier);
            return View(model);
        }


    }
}
