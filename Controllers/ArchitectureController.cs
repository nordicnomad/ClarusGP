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
    public class ArchitectureController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();

        //
        // GET: /Architecture/

        public ViewResult Index()
        {
            return View(db.ContentArchitectures.ToList());
        }

        //
        // GET: /Architecture/Details/5

        public ViewResult Details(int id)
        {
            ContentArchitecture contentarchitecture = db.ContentArchitectures.Single(c => c.ArchitectureID == id);
            return View(contentarchitecture);
        }

        //
        // GET: /Architecture/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Architecture/Create

        [HttpPost]
        public ActionResult Create(ContentArchitecture contentarchitecture)
        {
            if (ModelState.IsValid)
            {
                db.ContentArchitectures.AddObject(contentarchitecture);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(contentarchitecture);
        }
        
        //
        // GET: /Architecture/Edit/5
 
        public ActionResult Edit(int id)
        {
            ContentArchitecture contentarchitecture = db.ContentArchitectures.Single(c => c.ArchitectureID == id);
            return View(contentarchitecture);
        }

        //
        // POST: /Architecture/Edit/5

        [HttpPost]
        public ActionResult Edit(ContentArchitecture contentarchitecture)
        {
            if (ModelState.IsValid)
            {
                db.ContentArchitectures.Attach(contentarchitecture);
                db.ObjectStateManager.ChangeObjectState(contentarchitecture, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contentarchitecture);
        }

        //
        // GET: /Architecture/Delete/5
 
        public ActionResult Delete(int id)
        {
            ContentArchitecture contentarchitecture = db.ContentArchitectures.Single(c => c.ArchitectureID == id);
            return View(contentarchitecture);
        }

        //
        // POST: /Architecture/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ContentArchitecture contentarchitecture = db.ContentArchitectures.Single(c => c.ArchitectureID == id);
            db.ContentArchitectures.DeleteObject(contentarchitecture);
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