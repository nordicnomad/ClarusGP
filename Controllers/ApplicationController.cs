using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClarusBlogBoard.Models;
using PagedList;

namespace ClarusBlogBoard.Controllers
{
    [Authorize(Roles = "admin, superadmin")]
    public class ApplicationController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();

        //
        // GET: /Application/

        public ViewResult Index(string Job = "", string Status = "", string Assigned = "")
        {
            var applications = db.Applications.Include("ApplicationStatus").Include("AssignedTo").Include("JobApplicant").Include("JobPost").ToList();

            if (Job != "")
            {
                applications = (from a in applications
                                where a.ApplicationJobID.ToString() == Job &&
                                (a.JobPost.JobPostStatusID.ToString() == "2" ||
                                a.JobPost.JobPostStatusID.ToString() == "1")
                                select a).ToList();
            }

            if (Status != "")
            {
                applications = (from a in applications
                            where a.ApplicationStatus.AppStatusID.ToString() == Status
                            select a).ToList();
            }

            if (Assigned != "")
            {
                applications = (from a in applications
                            where a.ApplicationAssignedID.ToString() == Assigned
                            select a).ToList();
            }

            ViewBag.Job = new SelectList(db.JobPosts, "JobID", "JobName");
            ViewBag.Status = new SelectList(db.ApplicationStatus1, "AppStatusID", "ApplicationStatusName");
            ViewBag.Assigned = new SelectList(db.AssignedToes, "AssignedID", "AssignedName");

            return View(applications.ToList());
        }

        //
        // GET: /Application/Details/5

        public ViewResult Details(int id)
        {
            Application application = db.Applications.Single(a => a.ApplicationID == id);
            return View(application);
        }

        //
        // GET: /Application/Create

        public ActionResult Create()
        {
            ViewBag.ApplicationStatusID = new SelectList(db.ApplicationStatus1, "AppStatusID", "ApplicationStatusName");
            ViewBag.ApplicationAssignedID = new SelectList(db.AssignedToes, "AssignedID", "AssignedName");
            ViewBag.ApplicationApplicantID = new SelectList(db.JobApplicants, "ApplicantID", "ApplicantLastName");
            ViewBag.ApplicationJobID = new SelectList(db.JobPosts, "JobID", "JobName");
            return View();
        } 

        //
        // POST: /Application/Create

        [HttpPost]
        public ActionResult Create(Application application)
        {
            if (ModelState.IsValid)
            {
                
                db.Applications.AddObject(application);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ApplicationStatusID = new SelectList(db.ApplicationStatus1, "AppStatusID", "ApplicationStatusName", application.ApplicationStatusID);
            ViewBag.ApplicationAssignedID = new SelectList(db.AssignedToes, "AssignedID", "AssignedName", application.ApplicationAssignedID);
            ViewBag.ApplicationApplicantID = new SelectList(db.JobApplicants, "ApplicantID", "ApplicantLastName", application.ApplicationApplicantID);
            ViewBag.ApplicationJobID = new SelectList(db.JobPosts, "JobID", "JobName", application.ApplicationJobID);
            return View(application);
        }
        
        //
        // GET: /Application/Edit/5
 
        public ActionResult Edit(int id)
        {
            Application application = db.Applications.Single(a => a.ApplicationID == id);
            ViewBag.ApplicationStatusID = new SelectList(db.ApplicationStatus1, "AppStatusID", "ApplicationStatusName", application.ApplicationStatusID);
            ViewBag.ApplicationAssignedID = new SelectList(db.AssignedToes, "AssignedID", "AssignedName", application.ApplicationAssignedID);
            ViewBag.ApplicationApplicantID = new SelectList(db.JobApplicants, "ApplicantID", "ApplicantLastName", application.ApplicationApplicantID);
            ViewBag.ApplicationJobID = new SelectList(db.JobPosts, "JobID", "JobName", application.ApplicationJobID);
            return View(application);
        }

        //
        // POST: /Application/Edit/5

        [HttpPost]
        public ActionResult Edit(Application application)
        {
            if (ModelState.IsValid)
            {
                db.Applications.Attach(application);
                db.ObjectStateManager.ChangeObjectState(application, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationStatusID = new SelectList(db.ApplicationStatus1, "AppStatusID", "ApplicationStatusName", application.ApplicationStatusID);
            ViewBag.ApplicationAssignedID = new SelectList(db.AssignedToes, "AssignedID", "AssignedName", application.ApplicationAssignedID);
            ViewBag.ApplicationApplicantID = new SelectList(db.JobApplicants, "ApplicantID", "ApplicantLastName", application.ApplicationApplicantID);
            ViewBag.ApplicationJobID = new SelectList(db.JobPosts, "JobID", "JobName", application.ApplicationJobID);
            return View(application);
        }

        //
        // GET: /Application/Delete/5
 
        public ActionResult Delete(int id)
        {
            Application application = db.Applications.Single(a => a.ApplicationID == id);
            return View(application);
        }

        //
        // POST: /Application/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Application application = db.Applications.Single(a => a.ApplicationID == id);
            db.Applications.DeleteObject(application);
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