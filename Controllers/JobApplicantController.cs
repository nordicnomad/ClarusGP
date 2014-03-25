using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClarusBlogBoard.Models;
using System.Collections;
using System.Net.Mail;

namespace ClarusBlogBoard.Controllers
{ 
    public class JobApplicantController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();

        //
        // GET: /JobApplicant/
        [Authorize(Roles = "admin, superadmin")]
        public ViewResult Index(string searchString = "")
        {
            ViewBag.CurrentSearchString = searchString;

            var jobapplicants = db.JobApplicants.Include("ApplicantState").ToList();

            if (searchString != "") 
            {
                searchString = searchString.ToLower();

                jobapplicants = (from ja in jobapplicants
                                 where ja.ApplicantFirstName.ToString().ToLower().Contains(searchString) ||
                                 ja.ApplicantLastName.ToString().ToLower().Contains(searchString) ||
                                 ja.ApplicantCity.ToString().ToLower().Contains(searchString) ||
                                 ja.ApplicantState.StateName.ToString().ToLower().Contains(searchString) ||
                                 ja.ApplicantEmail.ToString().ToLower().Contains(searchString) || 
                                 ja.ResumeText.ToString().ToLower().Contains(searchString)
                                 select ja).ToList();
            }
            
            return View(jobapplicants.ToList());
        }

        //
        // GET: /JobApplicant/Details/5
        [Authorize(Roles = "admin, superadmin")]
        public ViewResult Details(int id)
        {
            JobApplicant jobapplicant = db.JobApplicants.Single(j => j.ApplicantID == id);
            return View(jobapplicant);
        }

        //
        // GET: /JobApplicant/Create

        public ActionResult Create(int CurrentJobID, string CurrentJobName)
        {
            
            
            ViewBag.CurrentJobName = CurrentJobName;
            ViewBag.CurrentJob = CurrentJobID;
            ViewBag.ApplicantStateID = new SelectList(db.ApplicantStates, "StateID", "StateName");
            
            return View();
        } 

        //
        // POST: /JobApplicant/Create

        [HttpPost]
        public ActionResult Create(JobApplicant jobapplicant, int CurrentJobID)
        {

            var jobposts = db.JobPosts.Include("AssignedTo").ToList();

            var jobapplied = (from j in jobposts
                             where j.JobID == CurrentJobID
                             select j).Single();

            if (ModelState.IsValid)
            {
                
                db.JobApplicants.AddObject(jobapplicant);
                db.SaveChanges();
                int JobApplicantID = jobapplicant.ApplicantID;
                Application application = new Application();
                application.ApplicationApplicantID = JobApplicantID;
                application.ApplicationJobID = CurrentJobID;
                application.ApplicationStatusID = 1;
                db.Applications.AddObject(application);
                db.SaveChanges();

                if (jobapplied.JobOfficeID < 4)
                {
                    string recipient = "recruit.kc@clarusgp.com";
                    string subject = "Application Alert Notification";
                    if(jobapplied.JobOfficeID == 1)
                    {
                        recipient = "recruit.kc@clarusgp.com";
                        subject = "Kansas City Office: Application Alert Notification";
                    }
                    if (jobapplied.JobOfficeID == 2)
                    {
                        recipient = "recruit.denver@clarusgp.com";
                        subject = "Denver Office: Application Alert Notification";
                    }
                    if (jobapplied.JobOfficeID == 3)
                    {
                        recipient = "recruit.baltimore@clarusgp.com";
                        subject = "Baltimore Office: Application Alert Notification";
                    }
                    if (jobapplied.RecruiterAssignedTo != null && jobapplied.AssignedTo.AssignedEmail != null)
                    {
                        recipient = jobapplied.AssignedTo.AssignedEmail.ToString();
                        subject = "Application Alert Notification For: " + jobapplied.AssignedTo.AssignedName.ToString();
                    }
                    string body = string.Format("{0} {1} has applied for {3}.<br /> <a href=\"http://www.clarusgp.com/Application/Details/" + "{2}\">Click Here to View the Application</a>.", jobapplicant.ApplicantFirstName, jobapplicant.ApplicantLastName, application.ApplicationID, jobapplied.JobName);

                    MailMessage msg = new MailMessage
                        ("info@clarusgp.com", recipient,
                        subject, body);

                    msg.Priority = MailPriority.High;
                    msg.IsBodyHtml = true;

                    try
                    {
                        SmtpClient client = new SmtpClient("relay-hosting.secureserver.net");
                        client.Send(msg);
                    }
                    catch { }
                }

                return RedirectToAction("Upload", "JobApplicant", new {id = jobapplicant.ApplicantID});  

            }

            ViewBag.ApplicantStateID = new SelectList(db.ApplicantStates, "StateID", "StateName", jobapplicant.ApplicantStateID);
            return View(jobapplicant);
        }

        public ActionResult Upload(int id)
        {
            JobApplicant jobapplicant = db.JobApplicants.Single(j => j.ApplicantID == id);
            ViewBag.ApplicantStateID = new SelectList(db.ApplicantStates, "StateID", "StateName", jobapplicant.ApplicantStateID);
            return View(jobapplicant);
        }

        [HttpPost]
        public ActionResult Upload(JobApplicant jobapplicant, HttpPostedFileBase resume)
        {
            if (ModelState.IsValid)
            {
                if (resume != null)
                {
                    //get the file name
                    string resumeName = resume.FileName;

                    //strip the extension from the file name
                    string ext = resumeName.Substring(resumeName.LastIndexOf("."));

                    //generate a guid for the new image name
                    string resumeRename = Guid.NewGuid().ToString();

                    resumeRename += ext;

                    //save the image to the productImages folder
                    resume.SaveAs(Server.MapPath("~/Content/resumes/" + resumeRename));

                    //save the imageName to the Product Object
                    jobapplicant.ResumePath = resumeRename;
                }

                if (jobapplicant.ResumeText == null)
                {
                    jobapplicant.ResumeText = "No Text Resume";
                }

                db.JobApplicants.Attach(jobapplicant);
                db.ObjectStateManager.ChangeObjectState(jobapplicant, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Careers", "JobPost", new { isApplication = true });  
            } 
            //new { name = jobapplicant.ApplicantFirstName }
            ViewBag.ApplicantStateID = new SelectList(db.ApplicantStates, "StateID", "StateName", jobapplicant.ApplicantStateID);
            return View(jobapplicant);
        }
        
        //
        // GET: /JobApplicant/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        public ActionResult Edit(int id)
        {
            JobApplicant jobapplicant = db.JobApplicants.Single(j => j.ApplicantID == id);
            ViewBag.ApplicantStateID = new SelectList(db.ApplicantStates, "StateID", "StateName", jobapplicant.ApplicantStateID);
            return View(jobapplicant);
        }

        //
        // POST: /JobApplicant/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        [HttpPost]
        public ActionResult Edit(JobApplicant jobapplicant, HttpPostedFileBase resume)
        {
            if (ModelState.IsValid)
            {

                if (resume != null)
                {
                    //get the file name
                    string resumeName = resume.FileName;

                    //strip the extension from the file name
                    string ext = resumeName.Substring(resumeName.LastIndexOf("."));

                    //generate a guid for the new image name
                    string resumeRename = Guid.NewGuid().ToString();

                    resumeRename += ext;

                    //save the image to the productImages folder
                    resume.SaveAs(Server.MapPath("~/Content/resumes/" + resumeRename));

                    //save the imageName to the Product Object
                    jobapplicant.ResumePath = resumeRename;
                }
                else
                {
                    jobapplicant.ResumePath = (from r in db.JobApplicants
                                              where r.ApplicantID == jobapplicant.ApplicantID
                                              select r.ResumePath).Single();  
                }

                db.JobApplicants.Attach(jobapplicant);
                db.ObjectStateManager.ChangeObjectState(jobapplicant, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicantStateID = new SelectList(db.ApplicantStates, "StateID", "StateName", jobapplicant.ApplicantStateID);
            return View(jobapplicant);
        }

        //
        // GET: /JobApplicant/Delete/5
        [Authorize(Roles = "admin, superadmin")]
        public ActionResult Delete(int id)
        {
            JobApplicant jobapplicant = db.JobApplicants.Single(j => j.ApplicantID == id);
            return View(jobapplicant);
        }

        //
        // POST: /JobApplicant/Delete/5
        [Authorize(Roles = "admin, superadmin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            JobApplicant jobapplicant = db.JobApplicants.Single(j => j.ApplicantID == id);
            db.JobApplicants.DeleteObject(jobapplicant);
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