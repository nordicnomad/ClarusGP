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
    public class DevelopmentController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();

        
        //
        // GET: /Development/Details/1

        public ViewResult Details(int id)
        {
            ContentDevelopment contentdevelopment = db.ContentDevelopments.Single(c => c.DevelopmentID == id);
            return View(contentdevelopment);
        }

        //
        // GET: /Development/Edit/1
 
        public ActionResult Edit(int id)
        {
            ContentDevelopment contentdevelopment = db.ContentDevelopments.Single(c => c.DevelopmentID == id);
            return View(contentdevelopment);
        }

        //
        // POST: /Development/Edit/1

        [HttpPost]
        public ActionResult Edit(ContentDevelopment contentdevelopment)
        {
            if (ModelState.IsValid)
            {
                db.ContentDevelopments.Attach(contentdevelopment);
                db.ObjectStateManager.ChangeObjectState(contentdevelopment, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(contentdevelopment);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}