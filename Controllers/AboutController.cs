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
    public class AboutController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();

        //
        // GET: /About/Details/5
        
        public ViewResult Details(int id)
        {
            ContentAbout contentabout = db.ContentAbouts.Single(c => c.AboutID == id);
            return View(contentabout);
        }

        //
        // GET: /About/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        public ActionResult Edit(int id)
        {
            ContentAbout contentabout = db.ContentAbouts.Single(c => c.AboutID == id);
            return View(contentabout);
        }

        //
        // POST: /About/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        [HttpPost]
        public ActionResult Edit(ContentAbout contentabout)
        {
            if (ModelState.IsValid)
            {
                db.ContentAbouts.Attach(contentabout);
                db.ObjectStateManager.ChangeObjectState(contentabout, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = 1 });
            }
            return View(contentabout);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}