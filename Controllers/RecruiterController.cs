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
    [Authorize(Roles = "superadmin")]
    public class RecruiterController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();

        //
        // GET: /Recruiter/

        public ViewResult Index()
        {
            return View(db.AssignedToes.ToList());
        }

        //
        // GET: /Recruiter/Details/5

        public ViewResult Details(int id)
        {
            AssignedTo assignedto = db.AssignedToes.Single(a => a.AssignedID == id);
            return View(assignedto);
        }

        //
        // GET: /Recruiter/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Recruiter/Create

        [HttpPost]
        public ActionResult Create(AssignedTo assignedto)
        {
            if (ModelState.IsValid)
            {
                db.AssignedToes.AddObject(assignedto);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(assignedto);
        }
        
        //
        // GET: /Recruiter/Edit/5
 
        public ActionResult Edit(int id)
        {
            AssignedTo assignedto = db.AssignedToes.Single(a => a.AssignedID == id);
            return View(assignedto);
        }

        //
        // POST: /Recruiter/Edit/5

        [HttpPost]
        public ActionResult Edit(AssignedTo assignedto)
        {
            if (ModelState.IsValid)
            {
                db.AssignedToes.Attach(assignedto);
                db.ObjectStateManager.ChangeObjectState(assignedto, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assignedto);
        }

        //
        // GET: /Recruiter/Delete/5
 
        public ActionResult Delete(int id)
        {
            AssignedTo assignedto = db.AssignedToes.Single(a => a.AssignedID == id);
            return View(assignedto);
        }

        //
        // POST: /Recruiter/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            AssignedTo assignedto = db.AssignedToes.Single(a => a.AssignedID == id);
            db.AssignedToes.DeleteObject(assignedto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}