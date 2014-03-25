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
    
    public class JobPostController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();

        public ViewResult Jobs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Jobs(string jobSearch)
        {
            return RedirectToAction("Careers", new { searchString = jobSearch }); 
        }

        public ViewResult Careers(string searchString = "", string Category = "", string Location = "", int page = 1, bool isApplication = false)
        {
            
            ViewBag.CurrentCategory = Category;
            ViewBag.CurrentLocation = Location;
            ViewBag.CurrentSearchString = searchString;
            ViewBag.isApplication = isApplication; 


            if (Request.HttpMethod == "Post")
            {
                page = 1;
            }

            int pageSize = 20;

            var jobposts = db.JobPosts.Include("JobCategory").Include("JobPostStatus").Include("OfficeAssigned").ToList();
            //var jobs = db.JobPosts
            //    .Include("JobCategory")
            //    .Include("JobPostStatus")
            //    .Include("OfficeAssigned").ToList();

            if (!User.IsInRole("admin"))
            {
                jobposts = (from j in jobposts
                            where j.JobPostStatusID == 2
                            orderby j.JobName
                            select j).ToList();
            }

            if (searchString != "") 
            {
                searchString = searchString.ToLower();

                jobposts = (from j in jobposts
                        where j.JobName.ToLower().Contains(searchString) ||
                        j.JobDescription.ToLower().Contains(searchString) ||
                        j.JobCategory.CategoryName.ToLower().Contains(searchString) ||
                        j.LocationDescription.ToLower().Contains(searchString)
                        orderby j.JobName
                        select j).ToList();
            }

            if (Category != "")
            {
                jobposts = (from j in jobposts
                        where j.JobCategoryID.ToString() == Category
                        orderby j.JobName
                        select j).ToList();
            }

            if (Location != "")
            {
                jobposts = (from j in jobposts
                        where j.JobOfficeID.ToString() == Location
                        orderby j.JobName
                        select j).ToList();
            }

            ViewBag.Category = new SelectList(db.JobCategories, "CategoryID", "CategoryName");
            ViewBag.Location = new SelectList(db.OfficeAssigneds, "OfficeID", "OfficeName");
            


            //var jobposts = db.JobPosts.Include("JobCategory").Include("JobPostStatus").Include("OfficeAssigned");
            //return View(jobposts.ToList());
            return View(jobposts.ToPagedList(page, pageSize));
        }

        //
        // GET: /JobPost/
        [Authorize(Roles = "admin, superadmin")]
        public ViewResult Index(string Office = "", string Status = "", string Category = "", string Recruiter = "")
        {
            var jobposts = db.JobPosts.Include("JobCategory").Include("JobPostStatus").Include("OfficeAssigned").Include("AssignedTo").ToList();
            

            if (Office != "")
            {
                jobposts = (from j in jobposts
                                where j.JobOfficeID.ToString() == Office
                                orderby j.JobName
                                select j).ToList();
            }

            if (Status != "")
            {
                jobposts = (from j in jobposts
                                where j.JobPostStatusID.ToString() == Status
                                orderby j.JobName
                                select j).ToList();
            }

            if (Category != "")
            {
                jobposts = (from j in jobposts
                                where j.JobCategoryID.ToString() == Category
                                orderby j.JobName
                                select j).ToList();
            }

            if (Recruiter != "")
            {
                jobposts = (from j in jobposts
                                where j.RecruiterAssignedTo.ToString() == Recruiter
                                orderby j.JobName
                                select j).ToList();
            }

            ViewBag.Office = new SelectList(db.OfficeAssigneds, "OfficeID", "OfficeName");
            ViewBag.Status = new SelectList(db.JobPostStatus1, "PostStatusID", "JobPostStatusName");
            ViewBag.Category = new SelectList(db.JobCategories, "CategoryID", "CategoryName");
            ViewBag.Recruiter = new SelectList(db.AssignedToes, "AssignedID", "AssignedName");
            
            return View(jobposts.ToList());
        }

        //
        // GET: /JobPost/Details/5
        
        public ViewResult Details(int id)
        {
            JobPost jobpost = db.JobPosts.Single(j => j.JobID == id);
            return View(jobpost);
        }

        //
        // GET: /JobPost/Create
        [Authorize(Roles = "admin, superadmin")]
        public ActionResult Create()
        {
            ViewBag.JobCategoryID = new SelectList(db.JobCategories, "CategoryID", "CategoryName");
            ViewBag.JobPostStatusID = new SelectList(db.JobPostStatus1, "PostStatusID", "JobPostStatusName");
            ViewBag.JobOfficeID = new SelectList(db.OfficeAssigneds, "OfficeID", "OfficeName");
            ViewBag.RecruiterAssignedTo = new SelectList(db.AssignedToes, "AssignedID", "AssignedName");
            return View();
        }

        //
        // POST: /JobPost/Create
        [Authorize(Roles = "admin, superadmin")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(JobPost jobpost)
        {
            if (ModelState.IsValid)
            {
                db.JobPosts.AddObject(jobpost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JobCategoryID = new SelectList(db.JobCategories, "CategoryID", "CategoryName", jobpost.JobCategoryID);
            ViewBag.JobPostStatusID = new SelectList(db.JobPostStatus1, "PostStatusID", "JobPostStatusName", jobpost.JobPostStatusID);
            ViewBag.JobOfficeID = new SelectList(db.OfficeAssigneds, "OfficeID", "OfficeName", jobpost.JobOfficeID);
            ViewBag.RecruiterAssignedTo = new SelectList(db.AssignedToes, "AssignedID", "AssignedName", jobpost.RecruiterAssignedTo);
            return View(jobpost);
        }

        //
        // GET: /JobPost/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        public ActionResult Edit(int id)
        {
            JobPost jobpost = db.JobPosts.Single(j => j.JobID == id);
            ViewBag.JobCategoryID = new SelectList(db.JobCategories, "CategoryID", "CategoryName", jobpost.JobCategoryID);
            ViewBag.JobPostStatusID = new SelectList(db.JobPostStatus1, "PostStatusID", "JobPostStatusName", jobpost.JobPostStatusID);
            ViewBag.JobOfficeID = new SelectList(db.OfficeAssigneds, "OfficeID", "OfficeName", jobpost.JobOfficeID);
            ViewBag.RecruiterAssignedTo = new SelectList(db.AssignedToes, "AssignedID", "AssignedName", jobpost.RecruiterAssignedTo);
            return View(jobpost);
        }

        //
        // POST: /JobPost/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(JobPost jobpost)
        {
            if (ModelState.IsValid)
            {
                db.JobPosts.Attach(jobpost);
                db.ObjectStateManager.ChangeObjectState(jobpost, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobCategoryID = new SelectList(db.JobCategories, "CategoryID", "CategoryName", jobpost.JobCategoryID);
            ViewBag.JobPostStatusID = new SelectList(db.JobPostStatus1, "PostStatusID", "JobPostStatusName", jobpost.JobPostStatusID);
            ViewBag.JobOfficeID = new SelectList(db.OfficeAssigneds, "OfficeID", "OfficeName", jobpost.JobOfficeID);
            ViewBag.RecruiterAssignedTo = new SelectList(db.AssignedToes, "AssignedID", "AssignedName", jobpost.RecruiterAssignedTo);
            return View(jobpost);
        }

        //
        // GET: /JobPost/Delete/5
        [Authorize(Roles = "admin, superadmin")]
        public ActionResult Delete(int id)
        {
            JobPost jobpost = db.JobPosts.Single(j => j.JobID == id);
            return View(jobpost);
        }

        //
        // POST: /JobPost/Delete/5
        [Authorize(Roles = "admin, superadmin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            JobPost jobpost = db.JobPosts.Single(j => j.JobID == id);
            db.JobPosts.DeleteObject(jobpost);
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