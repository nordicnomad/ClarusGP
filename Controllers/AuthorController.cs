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
    public class AuthorController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();

        //
        // GET: /Author/

        public ViewResult Index()
        {
            return View(db.BlogAuthors.ToList());
        }

        //
        // GET: /Author/Details/5

        public ViewResult Details(int id)
        {
            BlogAuthor blogauthor = db.BlogAuthors.Single(b => b.AuthorID == id);
            return View(blogauthor);
        }

        //
        // GET: /Author/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Author/Create

        [HttpPost]
        public ActionResult Create(BlogAuthor blogauthor)
        {
            if (ModelState.IsValid)
            {
                db.BlogAuthors.AddObject(blogauthor);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(blogauthor);
        }
        
        //
        // GET: /Author/Edit/5
 
        public ActionResult Edit(int id)
        {
            BlogAuthor blogauthor = db.BlogAuthors.Single(b => b.AuthorID == id);
            return View(blogauthor);
        }

        //
        // POST: /Author/Edit/5

        [HttpPost]
        public ActionResult Edit(BlogAuthor blogauthor)
        {
            if (ModelState.IsValid)
            {
                db.BlogAuthors.Attach(blogauthor);
                db.ObjectStateManager.ChangeObjectState(blogauthor, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogauthor);
        }

        //
        // GET: /Author/Delete/5
 
        public ActionResult Delete(int id)
        {
            BlogAuthor blogauthor = db.BlogAuthors.Single(b => b.AuthorID == id);
            return View(blogauthor);
        }

        //
        // POST: /Author/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            BlogAuthor blogauthor = db.BlogAuthors.Single(b => b.AuthorID == id);
            db.BlogAuthors.DeleteObject(blogauthor);
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