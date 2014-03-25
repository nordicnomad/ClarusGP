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
    public class LegalController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();

        //
        // GET: /Legal/Details/5

        public ViewResult Details(int id)
        {
            ContentLegal contentlegal = db.ContentLegals.Single(c => c.LegalID == id);
            return View(contentlegal);
        }

        //
        // GET: /Legal/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        public ActionResult Edit(int id)
        {
            ContentLegal contentlegal = db.ContentLegals.Single(c => c.LegalID == id);
            return View(contentlegal);
        }

        //
        // POST: /Legal/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        [HttpPost]
        public ActionResult Edit(ContentLegal contentlegal)
        {
            if (ModelState.IsValid)
            {
                db.ContentLegals.Attach(contentlegal);
                db.ObjectStateManager.ChangeObjectState(contentlegal, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = 1 });
            }
            return View(contentlegal);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}