using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheAncientInn.Models;

namespace TheAncientInn.Controllers
{
    [Authorize]
    public class Tbl_ProductsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Tbl_Products -----------------------------------------------------
        public ActionResult Index()
        {    
            return View(db.Tbl_Products.ToList());
        }

        // GET: Tbl_Products/Details/5 -------------------------------------------
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Products tbl_Products = db.Tbl_Products.Find(id);
            if (tbl_Products == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Products);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Details([Bind(Include = "Id_Product, Adress_Client, Note_Client, ")] Tbl_Orders order)
        {
            order.Order_Time = DateTime.Now;
            order.Delivering_Time = DateTime.Now;
            order.Id_Product = Int32.Parse(Session["Id_User"].ToString());

            db.Tbl_Orders.Add(order);
            db.SaveChanges();

            return RedirectToAction("Index");

        }


        // GET: Tbl_Products/Create ----------------------------------------------

        [Authorize(Roles = "Admin")]

        public ActionResult Create()
        {
            ViewBag.Id_Ingredients = new SelectList(db.Tbl_Ingredients, "Id_Ingredients", "Name_Ingredients");
            ViewBag.Id_Product = new SelectList(db.Tbl_Products, "Id_Product", "Nome_Product");
            return View();
        }

        // POST: Tbl_Products/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Product,Id_Ingredients,Nome_Product,Description_Product,PhotoUno,PhotoDue,PhotoTre,Price_Product,Quantity_Product,TimeOfDelivering")] Tbl_Products tbl_Products)
        {
            string Photo1 = "";
            HttpPostedFileBase archivo = Request.Files["PhotoUno"];

            // CREATE UPLOAD PHOTO
            if (ModelState.IsValid)
            {
                try
                {

                    if (archivo != null && archivo.ContentLength > 0)
                    {
                        try
                        {
                            Photo1 = Path.Combine(Server.MapPath("~/Content/Img/"), Path.GetFileName(archivo.FileName));
                            archivo.SaveAs(Photo1);

                        }
                        catch (Exception ex)
                        {
                            ViewBag.ErrorArchivo = ex;
                        }
                    }

                    db.Tbl_Products.Add(tbl_Products);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex;
                }

            }

            ViewBag.Id_Ingredients = new SelectList(db.Tbl_Ingredients, "Id_Ingredients", "Name_Ingredients");
            ViewBag.Id_Product = new SelectList(db.Tbl_Products, "Id_Product", "Nome_Product", tbl_Products.Id_Product);
            return View(tbl_Products);

        }

        // GET: Tbl_Products/Edit/5 ---------------------------------------------------------------------------------------------
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Products tbl_Products = db.Tbl_Products.Find(id);
            if (tbl_Products == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Ingredients = new SelectList(db.Tbl_Ingredients, "Id_Ingredients", "Name_Ingredients");
            ViewBag.Id_Product = new SelectList(db.Tbl_Products, "Id_Product", "Nome_Product", tbl_Products.Id_Product);
            return View(tbl_Products);
        }

        // POST: Tbl_Products/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Product,Id_Ingredients,Nome_Product,Description_Product,PhotoUno,PhotoDue,PhotoTre,Price_Product,Quantity_Product,TimeOfDelivering")] Tbl_Products tbl_Products)
        {
            string Photo1 = "";
            HttpPostedFileBase archivo = Request.Files["PhotoUno"];

            if (ModelState.IsValid)
            {
                try
                {

                    if (archivo != null && archivo.ContentLength > 0)
                    {
                        try
                        {
                            Photo1 = Path.Combine(Server.MapPath("~/Content/Img/"), Path.GetFileName(archivo.FileName));
                            archivo.SaveAs(Photo1);

                        }
                        catch (Exception ex)
                        {
                            ViewBag.ErrorArchivo = ex;
                        }
                    }

                    db.Entry(tbl_Products).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex;
                }

                return RedirectToAction("Index");
                
            }

            ViewBag.Id_Ingredients = new SelectList(db.Tbl_Ingredients, "Id_Ingredients", "Name_Ingredients");
            ViewBag.Id_Product = new SelectList(db.Tbl_Products, "Id_Product", "Nome_Product", tbl_Products.Id_Product);
            return View(tbl_Products);
        }

        // GET: Tbl_Products/Delete/5 --------------------------------------------------------------------------------------------
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Products tbl_Products = db.Tbl_Products.Find(id);
            if (tbl_Products == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Products);
        }

        // POST: Tbl_Products/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Products tbl_Products = db.Tbl_Products.Find(id);
            List<Tbl_Orders> Orders = db.Tbl_Orders.Where(o => o.Id_Order == id).ToList();

            foreach (Tbl_Orders Order in Orders)
                db.Tbl_Orders.Remove(Order);

            db.Tbl_Products.Remove(tbl_Products); //RIMOZIONE PRODOTTO

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
