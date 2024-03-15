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
    public class Tbl_IngredientsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Tbl_Ingredients
        public ActionResult Index()
        {
            var tbl_Ingredients = db.Tbl_Ingredients.Include(t => t.Tbl_Ingredients1);
            return View(tbl_Ingredients.ToList());
        }

        // GET: Tbl_Ingredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Ingredients tbl_Ingredients = db.Tbl_Ingredients.Find(id);
            if (tbl_Ingredients == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Ingredients);
        }

        // GET: Tbl_Ingredients/Create
        public ActionResult Create()
        {
            ViewBag.Id_Ingredients = new SelectList(db.Tbl_Ingredients, "Id_Ingredients", "Name_Ingredients");
            return View();
        }

        // POST: Tbl_Ingredients/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Ingredients,Name_Ingredients")] Tbl_Ingredients tbl_Ingredients)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Ingredients.Add(tbl_Ingredients);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Ingredients = new SelectList(db.Tbl_Ingredients, "Id_Ingredients", "Name_Ingredients", tbl_Ingredients.Id_Ingredients);
            return View(tbl_Ingredients);
        }

        // GET: Tbl_Ingredients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Ingredients tbl_Ingredients = db.Tbl_Ingredients.Find(id);
            if (tbl_Ingredients == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Ingredients = new SelectList(db.Tbl_Ingredients, "Id_Ingredients", "Name_Ingredients", tbl_Ingredients.Id_Ingredients);
            return View(tbl_Ingredients);
        }

        // POST: Tbl_Ingredients/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Ingredients,Name_Ingredients")] Tbl_Ingredients tbl_Ingredients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Ingredients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Ingredients = new SelectList(db.Tbl_Ingredients, "Id_Ingredients", "Name_Ingredients", tbl_Ingredients.Id_Ingredients);
            return View(tbl_Ingredients);
        }

        // GET: Tbl_Ingredients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Ingredients tbl_Ingredients = db.Tbl_Ingredients.Find(id);
            if (tbl_Ingredients == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Ingredients);
        }

        // POST: Tbl_Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Ingredients tbl_Ingredients = db.Tbl_Ingredients.Find(id);
            db.Tbl_Ingredients.Remove(tbl_Ingredients);
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
