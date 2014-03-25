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
    public class ConsultingController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();
        
        public ViewResult Details(int id)
        {
            ContentConsulting contentconsulting = db.ContentConsultings.Single(c => c.ConsultingID == id);
            return View(contentconsulting);
        }

        //
        // GET: /Consulting/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        public ActionResult Edit(int id)
        {
            ContentConsulting contentconsulting = db.ContentConsultings.Single(c => c.ConsultingID == id);
            return View(contentconsulting);
        }

        //
        // POST: /Consulting/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        [HttpPost]
        public ActionResult Edit(ContentConsulting contentconsulting)
        {
            if (ModelState.IsValid)
            {
                db.ContentConsultings.Attach(contentconsulting);
                db.ObjectStateManager.ChangeObjectState(contentconsulting, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = 1});
            }
            return View(contentconsulting);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}