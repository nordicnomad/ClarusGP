using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClarusBlogBoard.Models;

namespace ClarusBlogBoard.Controllers
{ 
    public class AboutUsController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();

        //
        // GET: /AboutUs/Details/5

        public ViewResult Details(int id)
        {
            ContentAboutU contentaboutu = db.ContentAboutUs.Single(c => c.AboutUsID == id);
            return View(contentaboutu);
        }
        
        //
        // GET: /AboutUs/Edit/5
 
        public ActionResult Edit(int id)
        {
            ContentAboutU contentaboutu = db.ContentAboutUs.Single(c => c.AboutUsID == id);
            return View(contentaboutu);
        }

        //
        // POST: /AboutUs/Edit/5

        [HttpPost]
        public ActionResult Edit(ContentAboutU contentaboutu)
        {
            if (ModelState.IsValid)
            {
                db.ContentAboutUs.Attach(contentaboutu);
                db.ObjectStateManager.ChangeObjectState(contentaboutu, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = 1 });
            }
            return View(contentaboutu);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}