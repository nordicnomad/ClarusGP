using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClarusBlogBoard.Models;
using System.Web.Security;

namespace ClarusBlogBoard.Controllers
{
    public class HomeController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();

        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [Authorize(Roles = "admin, superadmin")]
        public ActionResult AdminPanel()
        {
            var blogposts = db.BlogPosts.ToList();
            var jobposts = db.JobPosts.ToList();
            var applications = db.Applications.ToList();
            var applicants = db.JobApplicants.ToList();


            //Dashboard blog stats
            int blogCount = blogposts.Count();
            ViewBag.BlogCount = blogCount;

            double blogCat1 = (from b in blogposts
                                where b.BlogCategory.BlogCategoryID == 1 
                                && b.BlogStatusID == 2
                                select b).Count();
            double blogCat2 = (from b in blogposts
                               where b.BlogCategory.BlogCategoryID == 2
                               && b.BlogStatusID == 2
                               select b).Count();
            double blogCat3 = (from b in blogposts
                               where b.BlogCategory.BlogCategoryID == 3
                               && b.BlogStatusID == 2
                               select b).Count();
            double blogCat4 = (from b in blogposts
                               where b.BlogCategory.BlogCategoryID == 4 
                               && b.BlogStatusID == 2
                               select b).Count();
            double blogCat5 = (from b in blogposts
                               where b.BlogCategory.BlogCategoryID == 5
                               && b.BlogStatusID == 2
                               select b).Count();

            double blogCalc1 = (100 * (blogCat1 / blogCount));
            double blogPercent1 = blogCalc1;
            double blogCalc2 = (100 * (blogCat2 / blogCount));
            double blogPercent2 = blogCalc2;
            double blogCalc3 = (100 * (blogCat3 / blogCount));
            double blogPercent3 = blogCalc3;
            double blogCalc4 = (100 * (blogCat4 / blogCount));
            double blogPercent4 = blogCalc4;
            double blogCalc5 = (100 * (blogCat5 / blogCount));
            double blogPercent5 = blogCalc5;

            ViewBag.BlogCategory1 = blogCat1;
            ViewBag.BlogCat1Percent = blogPercent1;
            ViewBag.BlogCategory2 = blogCat2;
            ViewBag.BlogCat2Percent = blogPercent2;
            ViewBag.BlogCategory3 = blogCat3;
            ViewBag.BlogCat3Percent = blogPercent3;
            ViewBag.BlogCategory4 = blogCat4;
            ViewBag.BlogCat4Percent = blogPercent4;
            ViewBag.BlogCategory5 = blogCat5;
            ViewBag.BlogCat5Percent = blogPercent5;


            //Dashboard job stats
            int jobCount = jobposts.Count();
            ViewBag.JobCount = jobCount;

            double jobCat1 = (from j in jobposts
                              where j.JobCategory.CategoryID == 1
                              && j.JobPostStatusID == 2
                              select j).Count();
            double jobCat2 = (from j in jobposts
                              where j.JobCategory.CategoryID == 2
                              && j.JobPostStatusID == 2
                              select j).Count();
            double jobCat3 = (from j in jobposts
                              where j.JobCategory.CategoryID == 3
                              && j.JobPostStatusID == 2
                              select j).Count();
            double jobCat4 = (from j in jobposts
                              where j.JobCategory.CategoryID == 4
                              && j.JobPostStatusID == 2
                              select j).Count();
            double jobCat5 = (from j in jobposts
                              where j.JobCategory.CategoryID == 5
                              && j.JobPostStatusID == 2
                              select j).Count();
            double jobCat6 = (from j in jobposts
                              where j.JobCategory.CategoryID == 6
                              && j.JobPostStatusID == 2
                              select j).Count();
            double jobLoc1 = (from j in jobposts
                              where j.JobOfficeID == 1
                              && j.JobPostStatusID == 2
                              select j).Count();
            double jobLoc2 = (from j in jobposts
                              where j.JobOfficeID == 2
                              && j.JobPostStatusID == 2
                              select j).Count();
            double jobLoc3 = (from j in jobposts
                              where j.JobOfficeID == 3
                              && j.JobPostStatusID == 2
                              select j).Count();

            double jobCalc1 = (100 * (jobCat1 / jobCount));
            double jobPercent1 = jobCalc1;
            double jobCalc2 = (100 * (jobCat2 / jobCount));
            double jobPercent2 = jobCalc2;
            double jobCalc3 = (100 * (jobCat3 / jobCount));
            double jobPercent3 = jobCalc3;
            double jobCalc4 = (100 * (jobCat4 / jobCount));
            double jobPercent4 = jobCalc4;
            double jobCalc5 = (100 * (jobCat5 / jobCount));
            double jobPercent5 = jobCalc5;
            double jobCalc6 = (100 * (jobCat6 / jobCount));
            double jobPercent6 = jobCalc6;
            double jobLocCalc1 = (100 * (jobLoc1 / jobCount));
            double jobLocPercent1 = jobLocCalc1;
            double jobLocCalc2 = (100 * (jobLoc2 / jobCount));
            double jobLocPercent2 = jobLocCalc2;
            double jobLocCalc3 = (100 * (jobLoc3 / jobCount));
            double jobLocPercent3 = jobLocCalc3;

            ViewBag.JobCategory1 = jobCat1;
            ViewBag.JobCat1Percent = jobPercent1;
            ViewBag.JobCategory2 = jobCat2;
            ViewBag.JobCat2Percent = jobPercent2;
            ViewBag.JobCategory3 = jobCat3;
            ViewBag.JobCat3Percent = jobPercent3;
            ViewBag.JobCategory4 = jobCat4;
            ViewBag.JobCat4Percent = jobPercent4;
            ViewBag.JobCategory5 = jobCat5;
            ViewBag.JobCat5Percent = jobPercent5;
            ViewBag.JobCategory6 = jobCat6;
            ViewBag.JobCat6Percent = jobPercent6;

            ViewBag.JobLocation1 = jobLoc1;
            ViewBag.JobLoc1Percent = jobLocPercent1;
            ViewBag.JobLocation2 = jobLoc2;
            ViewBag.JobLoc2Percent = jobLocPercent2;
            ViewBag.JobLocation3 = jobLoc3;
            ViewBag.JobLoc3Percent = jobLocPercent3;

            //Dashboard application stats
            int appCount = applications.Count();
            ViewBag.AppCount = appCount;
            int seekerCount = applicants.Count();
            ViewBag.SeekerCount = seekerCount;

            double appCat1 = (from a in applications
                              where a.ApplicationStatusID == 1
                              select a).Count();
            double appCat2 = (from a in applications
                              where a.ApplicationStatusID == 2
                              select a).Count();
            double appCat3 = (from a in applications
                              where a.ApplicationStatusID == 3
                              select a).Count();
            double appCat4 = (from a in applications
                              where a.ApplicationStatusID == 4
                              select a).Count();
            double appCat5 = (from a in applications
                              where a.ApplicationStatusID == 5
                              select a).Count();
            double appCat6 = (from a in applications
                              where a.ApplicationStatusID == 6
                              select a).Count();
            double appCat7 = (from a in applications
                              where a.ApplicationStatusID == 7
                              select a).Count();
            double appCat8 = (from a in applications
                              where a.ApplicationStatusID == 8
                              select a).Count();
            double appCat9 = (from a in applications
                              where a.ApplicationStatusID == 9
                              select a).Count();

            double appCalc1 = (100 * (appCat1 / appCount));
            double appPercent1 = appCalc1;
            double appCalc2 = (100 * (appCat2 / appCount));
            double appPercent2 = appCalc2;
            double appCalc3 = (100 * (appCat3 / appCount));
            double appPercent3 = appCalc3;
            double appCalc4 = (100 * (appCat4 / appCount));
            double appPercent4 = appCalc4;
            double appCalc5 = (100 * (appCat5 / appCount));
            double appPercent5 = appCalc5;
            double appCalc6 = (100 * (appCat6 / appCount));
            double appPercent6 = appCalc6;
            double appCalc7 = (100 * (appCat7 / appCount));
            double appPercent7 = appCalc7;
            double appCalc8 = (100 * (appCat8 / appCount));
            double appPercent8 = appCalc8;
            double appCalc9 = (100 * (appCat9 / appCount));
            double appPercent9 = appCalc9;

            ViewBag.AppCategory1 = appCat1;
            ViewBag.AppCat1Percent = appPercent1;
            ViewBag.AppCategory2 = appCat2;
            ViewBag.AppCat2Percent = appPercent2;
            ViewBag.AppCategory3 = appCat3;
            ViewBag.AppCat3Percent = appPercent3;
            ViewBag.AppCategory4 = appCat4;
            ViewBag.AppCat4Percent = appPercent4;
            ViewBag.AppCategory5 = appCat5;
            ViewBag.AppCat5Percent = appPercent5;
            ViewBag.AppCategory6 = appCat6;
            ViewBag.AppCat6Percent = appPercent6;
            ViewBag.AppCategory7 = appCat7;
            ViewBag.AppCat7Percent = appPercent7;
            ViewBag.AppCategory8 = appCat8;
            ViewBag.AppCat8Percent = appPercent8;
            ViewBag.AppCategory9 = appCat9;
            ViewBag.AppCat9Percent = appPercent9;

            return View();
        }


        [Authorize(Roles = "superadmin")]
        public ActionResult RemoveUser() {
            RemoveModel model = new RemoveModel();
            ViewData["user"] = new SelectList(Membership.GetAllUsers(), model.user);
            ViewData["role"] = new SelectList(Roles.GetAllRoles(), model.role);
            return View();
        }

        [Authorize(Roles = "superadmin")]
        [HttpPost]
        public ActionResult RemoveUser(RemoveModel model) {

            if (ModelState.IsValid)
            {
                List<string> rolesList = new List<string> { };
                foreach (string role in Roles.GetAllRoles())
                {
                    if (role != null)
                    {
                        rolesList.Add(role);
                    }
                }
                foreach (var userRole in rolesList)
                {
                    try
                    {
                        Roles.RemoveUserFromRole(model.user, userRole);
                    }
                    catch 
                    {}
                }
                
                Roles.AddUserToRole(model.user, model.role);
                //FormsAuthentication.SetAuthCookie(model.user, false /* createPersistentCookie */);
                return RedirectToAction("AdminPanel", "Home");
            }


            return View(model);
        }
    }
}
