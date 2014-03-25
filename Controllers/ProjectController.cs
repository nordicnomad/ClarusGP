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
    public class ProjectController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();

        //
        // GET: /Project/Details/5

        public ViewResult Details(int id)
        {
            ContentProject contentproject = db.ContentProjects.Single(c => c.ProjectID == id);
            return View(contentproject);
        }

        //
        // GET: /Project/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        public ActionResult Edit(int id)
        {
            ContentProject contentproject = db.ContentProjects.Single(c => c.ProjectID == id);
            return View(contentproject);
        }

        //
        // POST: /Project/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        [HttpPost]
        public ActionResult Edit(ContentProject contentproject)
        {
            if (ModelState.IsValid)
            {
                db.ContentProjects.Attach(contentproject);
                db.ObjectStateManager.ChangeObjectState(contentproject, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = 1 });
            }
            return View(contentproject);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}