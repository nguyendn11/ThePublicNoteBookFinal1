using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThePublicNoteBook.Models;
using System.Configuration;

namespace ThePublicNoteBook.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        private ThePublicNoteBookEntities db = new ThePublicNoteBookEntities();
        // GET: Article
        public ActionResult Index()
        {
            int uid = int.Parse(User.Identity.Name);
            var articles = db.Articles.Include(a => a.User).Where(x => x.UserId == uid).OrderByDescending(x => x.DateUp);
            return View(articles.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Article article)
        {
            try
            {
                article.Image = "1.jpg";
                article.DateUp = DateTime.Now;
                article.Views = 0;
                article.UserId = int.Parse(User.Identity.Name);
                if (ModelState.IsValid)
                {
                    db.Articles.Add(article);
                    db.SaveChanges();
                    ViewBag.Message = "Created";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View(article);
        }
        public ActionResult View404()
        {
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("View404");
            }

            int uid = int.Parse(User.Identity.Name);

            Article article = (from a in db.Articles
                               where a.Id == id && a.UserId == uid
                               select a).SingleOrDefault();

            if (article == null)
            {
                return RedirectToAction("View404");
            }
            return View(article);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Article article, HttpPostedFileBase img)
        {
            try
            {
                int uid = int.Parse(User.Identity.Name);

                Article a = (from b in db.Articles
                             where b.Id == article.Id && b.UserId == uid
                             select b).SingleOrDefault();
                a.Title = article.Title;
                a.Description = article.Description;
                a.Content = article.Content;
                a.Active = article.Active;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e) { ViewBag.Message = e.Message; }


            return View(article);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            int uid = int.Parse(User.Identity.Name);
            if (uid != id)
            {
                article.Views = article.Views + 1;
            }
            return View(article);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int uid = int.Parse(User.Identity.Name);

            Article article = (from a in db.Articles
                               where a.Id == id && a.UserId == uid
                               select a).SingleOrDefault();

            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int uid = int.Parse(User.Identity.Name);

            Article article = (from a in db.Articles
                               where a.Id == id && a.UserId == uid
                               select a).SingleOrDefault();

            db.Articles.Remove(article);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}