using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheAncientInn.Models;

namespace TheAncientInn.Controllers
{
    public class Tbl_UsersController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Tbl_Users
        public ActionResult Index()
        {     
            return View();
        }

        // GET: Tbl_Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Users tbl_Users = db.Tbl_Users.Find(id);
            if (tbl_Users == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Users);
        }

        // GET: Tbl_Users/Create
        public ActionResult Create()
        {
            ViewBag.Id_Order = new SelectList(db.Tbl_Orders, "Id_Order", "Name_Client");
            ViewBag.Id_User = new SelectList(db.Tbl_Users, "Id_User", "Name_User");
            return View();
        }

        // POST: Tbl_Users/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_User,Id_Order,Name_User,Surname_User,Username_User,Email_User,Password_User,Role_User")] Tbl_Users tbl_Users)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Users.Add(tbl_Users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Order = new SelectList(db.Tbl_Orders, "Id_Order", "Name_Client");
            ViewBag.Id_User = new SelectList(db.Tbl_Users, "Id_User", "Name_User", tbl_Users.Id_User);
            return View(tbl_Users);
        }

        // GET: Tbl_Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Users tbl_Users = db.Tbl_Users.Find(id);
            if (tbl_Users == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Order = new SelectList(db.Tbl_Orders, "Id_Order", "Name_Client");
            ViewBag.Id_User = new SelectList(db.Tbl_Users, "Id_User", "Name_User", tbl_Users.Id_User);
            return View(tbl_Users);
        }

        // POST: Tbl_Users/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_User,Id_Order,Name_User,Surname_User,Username_User,Email_User,Password_User,Role_User")] Tbl_Users tbl_Users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Order = new SelectList(db.Tbl_Orders, "Id_Order", "Name_Client");
            ViewBag.Id_User = new SelectList(db.Tbl_Users, "Id_User", "Name_User", tbl_Users.Id_User);
            return View(tbl_Users);
        }

        // GET: Tbl_Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Users tbl_Users = db.Tbl_Users.Find(id);
            if (tbl_Users == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Users);
        }

        // POST: Tbl_Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Users tbl_Users = db.Tbl_Users.Find(id);
            db.Tbl_Users.Remove(tbl_Users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
