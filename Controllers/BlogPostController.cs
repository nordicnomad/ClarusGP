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
    
    public class BlogPostController : Controller
    {
        private BlogBoardEntities db = new BlogBoardEntities();


        public ActionResult Blog(string Category = "", string Status = "")
        {
            ViewBag.CurrentCategory = Category;
            ViewBag.CurrentStatus = Status;

            var blogposts = db.BlogPosts.Include("BlogAuthor").Include("BlogPostStatus").Include("BlogCategory").ToList();

            if (User.IsInRole("admin") || User.IsInRole("superadmin"))
            {
                blogposts = (from bp in blogposts
                             select bp).ToList();
            }
            else
            {
                blogposts = (from bp in blogposts
                             where bp.BlogStatusID == 2
                             select bp).ToList();
            }

            if (Category != "")
            {
                blogposts = (from bp in blogposts
                             where bp.CategoryID.ToString() == Category
                             select bp).ToList();
            }

            if (Status != "")
            {
                blogposts = (from bp in blogposts
                             where bp.BlogStatusID.ToString() == Status
                             select bp).ToList();
            }

            return View(blogposts.ToList());
        }

        public ActionResult Community()
        {
            var blogposts = db.BlogPosts.Include("BlogAuthor").Include("BlogPostStatus").Include("BlogCategory").ToList();

            var communityPosts = (from cp in blogposts
                                 where cp.BlogCategory.BlogCategoryID == 2 &&
                                 cp.BlogStatusID == 2
                                 select cp).ToList();

            //ViewBag.CommunityPosts = communityPosts;

            return View(communityPosts.ToList());
        }

        //
        // GET: /BlogPost/
        [Authorize(Roles = "admin, superadmin")]
        public ViewResult Index(string Author = "", string Status = "", string Category = "")
        {
            var blogposts = db.BlogPosts.Include("BlogAuthor").Include("BlogPostStatus").Include("BlogCategory").ToList();

            if (Status == "")
            {
                blogposts = (from b in blogposts
                                 where b.BlogStatusID.ToString() == "2"
                                 select b).ToList();
            }

            if (Author != "")
            {
                blogposts = (from b in blogposts
                            where b.BlogAuthorID.ToString() == Author
                            select b).ToList();
            }

            if (Status != "")
            {
                blogposts = (from b in blogposts
                            where b.BlogStatusID.ToString() == Status
                            select b).ToList();
            }

            if (Category != "")
            {
                blogposts = (from b in blogposts
                            where b.BlogCategory.BlogCategoryID.ToString() == Category
                            select b).ToList();
            }

            ViewBag.Author = new SelectList(db.BlogAuthors, "AuthorID", "AuthorName");
            ViewBag.Status = new SelectList(db.BlogPostStatus1, "PubStatusID", "BlogStatusName");
            ViewBag.Category = new SelectList(db.BlogCategories, "BlogCategoryID", "BlogCategoryName");

            return View(blogposts.ToList());
        }

        //
        // GET: /BlogPost/Details/5

        public ViewResult Details(int id)
        {
            BlogPost blogpost = db.BlogPosts.Single(b => b.BlogPostID == id);
            
            //recommended articles aside
            List<BlogPost> relatedArticles = new List<BlogPost>();

            var articlesCategory = (from b in db.BlogPosts
                                        where b.BlogStatusID == blogpost.BlogStatusID
                                        select b).ToList();

            foreach (var item in articlesCategory)
            {
                if (item.CategoryID == blogpost.CategoryID && item.BlogPostID != id)
                {
                    relatedArticles.Add(item);
                }
            }

            ViewBag.RelatedArticles = relatedArticles;

            //next article footer widget
            
            var nextArticle = (from n in db.BlogPosts
                                   where n.BlogPostID == blogpost.BlogPostID + 1
                                   select n).SingleOrDefault();
            
            

            if (nextArticle != null)
            {
                
                ViewBag.NextArticleTitle = nextArticle.BlogTitle;
                ViewBag.NextArticleID = nextArticle.BlogPostID;
            }
            else
            {
                ViewBag.NextArticleID = 0; 
                ViewBag.NextArticleTitle = "";
            }

            //relatedArticles.Add(db.BlogPosts.Where(a => a.CategoryID == blogpost.CategoryID);
            return View(blogpost);
        }

        //
        // GET: /BlogPost/Create
        [Authorize(Roles = "admin, superadmin")]

        public ActionResult Create()
        {
            ViewBag.BlogAuthorID = new SelectList(db.BlogAuthors, "AuthorID", "AuthorName");
            ViewBag.BlogStatusID = new SelectList(db.BlogPostStatus1, "PubStatusID", "BlogStatusName");
            ViewBag.CategoryID = new SelectList(db.BlogCategories, "BlogCategoryID", "BlogCategoryName");
            return View();
        }

        //
        // POST: /BlogPost/Create
        [Authorize(Roles = "admin, superadmin")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(BlogPost blogpost, HttpPostedFileBase image)
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
                    image.SaveAs(Server.MapPath("~/Content/img/blogImages/" + imageRename));

                    //save the imageName to the Product Object
                    blogpost.BlogImageURL = imageRename;
                }
                if (image == null)
                {
                    blogpost.BlogImageURL = "noPhoto.jpg";
                }

                db.BlogPosts.AddObject(blogpost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlogAuthorID = new SelectList(db.BlogAuthors, "AuthorID", "AuthorName", blogpost.BlogAuthorID);
            ViewBag.BlogStatusID = new SelectList(db.BlogPostStatus1, "PubStatusID", "BlogStatusName", blogpost.BlogStatusID);
            ViewBag.CategoryID = new SelectList(db.BlogCategories, "BlogCategoryID", "BlogCategoryName");
            return View(blogpost);
        }

        //
        // GET: /BlogPost/Edit/5
        [Authorize(Roles = "admin, superadmin")]

        public ActionResult Edit(int id)
        {
            BlogPost blogpost = db.BlogPosts.Single(b => b.BlogPostID == id);
            ViewBag.BlogAuthorID = new SelectList(db.BlogAuthors, "AuthorID", "AuthorName", blogpost.BlogAuthorID);
            ViewBag.BlogStatusID = new SelectList(db.BlogPostStatus1, "PubStatusID", "BlogStatusName", blogpost.BlogStatusID);
            ViewBag.CategoryID = new SelectList(db.BlogCategories, "BlogCategoryID", "BlogCategoryName");
            return View(blogpost);
        }

        //
        // POST: /BlogPost/Edit/5
        [Authorize(Roles = "admin, superadmin")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(BlogPost blogpost, HttpPostedFileBase image)
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
                    image.SaveAs(Server.MapPath("~/Content/img/blogImages/" + imageRename));

                    //save the imageName to the Product Object
                    blogpost.BlogImageURL = imageRename;
                }
                else
                {
                    blogpost.BlogImageURL = (from i in db.BlogPosts
                                               where i.BlogPostID == blogpost.BlogPostID
                                               select i.BlogImageURL).Single();
                }

                db.BlogPosts.Attach(blogpost);
                db.ObjectStateManager.ChangeObjectState(blogpost, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogAuthorID = new SelectList(db.BlogAuthors, "AuthorID", "AuthorName", blogpost.BlogAuthorID);
            ViewBag.BlogStatusID = new SelectList(db.BlogPostStatus1, "PubStatusID", "BlogStatusName", blogpost.BlogStatusID);
            ViewBag.CategoryID = new SelectList(db.BlogCategories, "BlogCategoryID", "BlogCategoryName");
            return View(blogpost);
        }

        //
        // GET: /BlogPost/Delete/5
        [Authorize(Roles = "admin, superadmin")]
        public ActionResult Delete(int id)
        {
            BlogPost blogpost = db.BlogPosts.Single(b => b.BlogPostID == id);
            return View(blogpost);
        }
        
        //
        // POST: /BlogPost/Delete/5
        [Authorize(Roles = "admin, superadmin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogpost = db.BlogPosts.Single(b => b.BlogPostID == id);
            db.BlogPosts.DeleteObject(blogpost);
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