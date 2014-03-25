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
    public class ClientsController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();

        //
        // GET: /Clients/Details/5

        public ViewResult Details(int id)
        {
            ContentClients contentclients = db.ContentClients1.Single(c => c.ClientsID == id);
            return View(contentclients);
        }

        //
        // GET: /Clients/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        public ActionResult Edit(int id)
        {
            ContentClients contentclients = db.ContentClients1.Single(c => c.ClientsID == id);
            return View(contentclients);
        }

        //
        // POST: /Clients/Edit/5

        [HttpPost]
        [Authorize(Roles = "admin, superadmin")]
        public ActionResult Edit(ContentClients contentclients)
        {
            if (ModelState.IsValid)
            {
                db.ContentClients1.Attach(contentclients);
                db.ObjectStateManager.ChangeObjectState(contentclients, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = 1 });
            }
            return View(contentclients);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}