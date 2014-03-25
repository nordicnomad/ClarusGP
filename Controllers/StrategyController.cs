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
    public class StrategyController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();

        
        //
        // GET: /Strategy/Details/5

        public ViewResult Details(int id)
        {
            ContentStrategy contentstrategy = db.ContentStrategies.Single(c => c.StrategyID == id);
            return View(contentstrategy);
        }
        
        //
        // GET: /Strategy/Edit/5
 
        public ActionResult Edit(int id)
        {
            ContentStrategy contentstrategy = db.ContentStrategies.Single(c => c.StrategyID == id);
            return View(contentstrategy);
        }

        //
        // POST: /Strategy/Edit/5

        [HttpPost]
        public ActionResult Edit(ContentStrategy contentstrategy)
        {
            if (ModelState.IsValid)
            {
                db.ContentStrategies.Attach(contentstrategy);
                db.ObjectStateManager.ChangeObjectState(contentstrategy, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(contentstrategy);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}