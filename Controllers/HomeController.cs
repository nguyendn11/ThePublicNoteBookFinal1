using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ThePublicNoteBook.Models;

namespace ThePublicNoteBook.Controllers
{
    public class HomeController : Controller
    {

        private ThePublicNoteBookEntities db = new ThePublicNoteBookEntities();
        private int pageSize = 10;                                              // giá trị mặc định kích cỡ size là 10 

        // GET: Home
        public ActionResult Index(int? page, string search, string searchText)
        {
            db.Articles.Add(new Article() { });                                                                 //Tạo constructor
            var articles = (from a in db.Articles.AsEnumerable()                                                //Lấy ra các notebook khi đã được active
                            where Filter(a)
                            orderby a.Id descending
                            select a);
            if (!String.IsNullOrEmpty(searchText))
            {
                search = searchText;
                page = 1;
            }
            int pageNumber = page ?? 1;

            if (!String.IsNullOrEmpty(search))
            {
                IEnumerable<Article> articles2 = articles.Where(a => a.Title.Contains(search)
                                              || a.Description.Contains(search)
                                             || a.User.UserName.Contains(search));                                        ///Tìm kiếm dữ liệu theo search

                ViewBag.Total = articles2.Count();
                return View(articles2.ToPagedList(pageNumber, pageSize));
            }
            var totalUser = db.Users.Count();
            ViewBag.TotalUser = totalUser;
            ViewBag.Search = search;
            var totalArticle = articles.Count();
            ViewBag.Total = totalArticle;
            return View(articles.ToPagedList(pageNumber, pageSize));

        }
        private bool Filter(Article a)
        {
            return a.Active;                                                                                             //// Trả về các article đã kích hoạt
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User data)
        {
            try
            {
                var userName = (from u in db.Users
                                where u.UserName.Equals(data.UserName)
                                select u).SingleOrDefault();
                if (userName == null)
                {
                    Session.Clear();
                    data.Avatar = "avatar.jpg";                            //Gán avatar mặc định
                    data.LevelId = 2;                                      //Gán level = 2 là người dùng
                    data.Active = true;
                    data.Password = MySecurity.EncryptPass(data.Password); //Mã hóa MD5
                    db.Users.Add(data);
                    db.SaveChanges();
                    Session["Name"] = data.UserName;
                    FormsAuthentication.SetAuthCookie(data.Id.ToString(), false);
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Message = "Tài khoản này đã tồn tại";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex;
            }
            return View(data);
        }
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AllowAnonymous]
        public ActionResult Login([Bind(Include = "UserName,Password")]User data)
        {
            //check login
            var user = (from u in db.Users
                        where u.UserName.Equals(data.UserName)
                        select u).SingleOrDefault();
            if (user != null)               //đúng tài khoản
            {
                if (user.Password.Equals(MySecurity.EncryptPass(data.Password)))
                {
                    FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);                 //đăng nhập được và lưu cookie                                     
                    Session["Name"] = user.FullName;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Invalid Password";
                }
            }
            else
            {
                ViewBag.Message = "User doesn't exist ! ";
            }
            return View(data);
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string oldPass, string newPass, string reNewPass)
        {
            if (newPass.Trim() == "")
            {
                return Content("Please enter new password");
            }
            if (newPass != reNewPass)
            {
                return Content("Renew password not correct !");
            }
            var user = (from u in db.Users
                        where u.Id.ToString().Equals(User.Identity.Name)
                        select u).SingleOrDefault();
            if (!user.Password.Equals(MySecurity.EncryptPass(oldPass)))
            {
                return Content("Old password incorrect");
            }
            user.Password = MySecurity.EncryptPass(newPass);
            db.SaveChanges();
            ViewBag.Message = "Success";
            return RedirectToAction("Index");

        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("Name");
            Session.Abandon();
            return RedirectToAction("Login");
        }

        [Authorize]
        public ActionResult Profile()
        {
            var user = (from u in db.Users
                        where u.Id.ToString().Equals(User.Identity.Name)
                        select u).SingleOrDefault();
            Session["Name"] = user.FullName;
            if (user == null)
            {
                return RedirectToAction("LogOut");
            }
            return View(user);
        }
        [HttpPost, Authorize]
        public ActionResult Profile(User data, HttpPostedFileBase avatarfile)
        {
            var user = (from u in db.Users
                        where u.Id.ToString().Equals(User.Identity.Name)
                        select u).SingleOrDefault();
            if (user == null)
            {
                return RedirectToAction("LogOut");
            }
            else
            {
                Session.Clear();
                user.FullName = data.FullName;
                user.Email = data.Email;
                user.Phone = data.Phone;
                user.Address = data.Address;
                Session["Name"] = user.FullName;
                db.SaveChanges();
                ViewBag.Message = "Your profile has been update";
            }
            return View(user);

        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return View("View404");
            }
            if (Session["A" + article.Id] == null)
            {
                Session["A" + article.Id] = true;
                article.Views++;
                db.SaveChanges();
            }
            return View(article);
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
        public ActionResult View404()
        {
            return View();
        }



    }
}