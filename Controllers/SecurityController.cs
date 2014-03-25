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
    public class SecurityController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();

        //
        // GET: /Security/Details/5

        public ViewResult Details(int id)
        {
            ContentSecurity contentsecurity = db.ContentSecurities.Single(c => c.SecurityID == id);
            return View(contentsecurity);
        }

        //
        // GET: /Security/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        public ActionResult Edit(int id)
        {
            ContentSecurity contentsecurity = db.ContentSecurities.Single(c => c.SecurityID == id);
            return View(contentsecurity);
        }

        //
        // POST: /Security/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        [HttpPost]
        public ActionResult Edit(ContentSecurity contentsecurity)
        {
            if (ModelState.IsValid)
            {
                db.ContentSecurities.Attach(contentsecurity);
                db.ObjectStateManager.ChangeObjectState(contentsecurity, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = 1 });
            }
            return View(contentsecurity);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}