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
    public class ServicesController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();

        //
        // GET: /Services/Details/5

        public ViewResult Details(int id)
        {
            ContentService contentservice = db.ContentServices.Single(c => c.ServicesID == id);
            return View(contentservice);
        }
        
        //
        // GET: /Services/Edit/5
 
        public ActionResult Edit(int id)
        {
            ContentService contentservice = db.ContentServices.Single(c => c.ServicesID == id);
            return View(contentservice);
        }

        //
        // POST: /Services/Edit/5

        [HttpPost]
        public ActionResult Edit(ContentService contentservice)
        {
            if (ModelState.IsValid)
            {
                db.ContentServices.Attach(contentservice);
                db.ObjectStateManager.ChangeObjectState(contentservice, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = 1 });
            }
            return View(contentservice);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}