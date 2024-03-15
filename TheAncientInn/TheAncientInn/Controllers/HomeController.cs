using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TheAncientInn.Models;

namespace TheAncientInn.Controllers
{
    public class HomeController : Controller
    {
        private ModelDbContext db = new ModelDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // LOGIN -------------------

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn([Bind(Include = "Username_User, Password_User ")] Tbl_Users user)
        {

            Tbl_Users User = db.Tbl_Users.Where(u => u.Username_User.Equals(user.Username_User) && u.Password_User.Equals(user.Password_User)).FirstOrDefault();

            if (User != null)
            {
                Session["Id_User"] = User.Id_User;

                FormsAuthentication.SetAuthCookie(User.Username_User, true);
                return RedirectToAction("Index");
            }
            return View();
        }

        // LOGOUT -------------------
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        // REGISTER -----------------
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn([Bind(Exclude = "Role_User")] Tbl_Users user)
        {
            if (ModelState.IsValid)
            {
                user.Role_User = "Username_User";
                db.Tbl_Users.Add(user);
                db.SaveChanges();

                Session["Id_User"] = user.Id_User;

                return RedirectToAction("Index");
            }

            return View();
        }

    }
}