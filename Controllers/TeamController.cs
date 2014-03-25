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
    public class TeamController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();

        //
        // GET: /Team/
        public ViewResult Team()
        {

            return View(db.ContentTeams.ToList());
        }

        [Authorize(Roles = "admin, superadmin")]
        public ViewResult Index()
        {
            return View(db.ContentTeams.ToList());
        }

        //
        // GET: /Team/Details/5
        [Authorize(Roles = "admin, superadmin")]
        public ViewResult Details(int id)
        {
            ContentTeam contentteam = db.ContentTeams.Single(c => c.TeamID == id);
            return View(contentteam);
        }

        //
        // GET: /Team/Create
        [Authorize(Roles = "admin, superadmin")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Team/Create
        [Authorize(Roles = "admin, superadmin")]
        [HttpPost]
        public ActionResult Create(ContentTeam contentteam, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    //get the file name
                    string imageName = image.FileName;

                    //strip the extension from the file name
                    string ext = imageName.Substring(imageName.LastIndexOf("."));

                    //generate a guid for the new image name
                    string imageRename = Guid.NewGuid().ToString();

                    imageRename += ext;

                    //save the image to the productImages folder
                    image.SaveAs(Server.MapPath("~/Content/img/teamPhotos/" + imageRename));

                    //save the imageName to the Product Object
                    contentteam.TeamImage = imageRename;
                }
                if (image == null)
                {
                    contentteam.TeamImage = "noPhoto.jpg";
                }

                db.ContentTeams.AddObject(contentteam);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(contentteam);
        }
        
        //
        // GET: /Team/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        public ActionResult Edit(int id)
        {
            ContentTeam contentteam = db.ContentTeams.Single(c => c.TeamID == id);
            return View(contentteam);
        }

        //
        // POST: /Team/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        [HttpPost]
        public ActionResult Edit(ContentTeam contentteam, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    //get the file name
                    string imageName = image.FileName;

                    //strip the extension from the file name
                    string ext = imageName.Substring(imageName.LastIndexOf("."));

                    //generate a guid for the new image name
                    string imageRename = Guid.NewGuid().ToString();

                    imageRename += ext;

                    //save the image to the productImages folder
                    image.SaveAs(Server.MapPath("~/Content/img/teamPhotos/" + imageRename));

                    //save the imageName to the Product Object
                    contentteam.TeamImage = imageRename;
                }
                else
                {
                    contentteam.TeamImage = (from i in db.ContentTeams
                                             where i.TeamID == contentteam.TeamID
                                             select i.TeamImage).Single();
                }

                db.ContentTeams.Attach(contentteam);
                db.ObjectStateManager.ChangeObjectState(contentteam, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contentteam);
        }

        //
        // GET: /Team/Delete/5
        [Authorize(Roles = "admin, superadmin")]
        public ActionResult Delete(int id)
        {
            ContentTeam contentteam = db.ContentTeams.Single(c => c.TeamID == id);
            return View(contentteam);
        }

        //
        // POST: /Team/Delete/5
        [Authorize(Roles = "admin, superadmin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ContentTeam contentteam = db.ContentTeams.Single(c => c.TeamID == id);
            db.ContentTeams.DeleteObject(contentteam);
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