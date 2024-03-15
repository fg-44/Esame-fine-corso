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
    [Authorize]
    public class Tbl_OrdersController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Tbl_Orders
        public ActionResult Index()
        {
            int id = Int32.Parse(Session["Id_User"].ToString());
            var tbl_Orders = db.Tbl_Orders.Include(t => t.Tbl_Products).Include(t => t.Tbl_Users); ;

            if (User.IsInRole("Admin"))
                return View(tbl_Orders.ToList());

            return View(db.Tbl_Orders.Where(o => o.Id_User == id && o.IsFulfilled == false));
        }

        // GET: Tbl_Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Orders tbl_Orders = db.Tbl_Orders.Find(id);
            if (tbl_Orders == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Orders);
        }


        // POST: Tbl_Orders/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Order,Id_User,Id_Product,Id_Ingredients,Adress_Client,IsConfirmed,IsFulfilled,Order_Time,Delivering_Time,Note_Client")] Tbl_Orders tbl_Orders)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Orders.Add(tbl_Orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Ingredients = new SelectList(db.Tbl_Ingredients, "Id_Ingredients", "Name_Ingredients", tbl_Orders.Id_Ingredients);
            ViewBag.Id_Order = new SelectList(db.Tbl_Orders, "Id_Order", "Adress_Client", tbl_Orders.Id_Order);
            ViewBag.Id_Product = new SelectList(db.Tbl_Products, "Id_Product", "Nome_Product", tbl_Orders.Id_Product);
            ViewBag.Id_User = new SelectList(db.Tbl_Users, "Id_User", "Name_User", tbl_Orders.Id_User);
            return View(tbl_Orders);
        }

        // GET: Tbl_Orders/Edit/5 ----------------------------------------------------------------------------
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Orders tbl_Orders = db.Tbl_Orders.Find(id);
            if (tbl_Orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Ingredients = new SelectList(db.Tbl_Ingredients, "Id_Ingredients", "Name_Ingredients", tbl_Orders.Id_Ingredients);
            ViewBag.Id_Order = new SelectList(db.Tbl_Orders, "Id_Order", "Adress_Client", tbl_Orders.Id_Order);
            ViewBag.Id_Product = new SelectList(db.Tbl_Products, "Id_Product", "Nome_Product", tbl_Orders.Id_Product);
            ViewBag.Id_User = new SelectList(db.Tbl_Users, "Id_User", "Name_User", tbl_Orders.Id_User);
            return View(tbl_Orders);
        }

        // POST: Tbl_Orders/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Order,Id_User,Id_Product,Id_Ingredients,Adress_Client,IsConfirmed,IsFulfilled,Order_Time,Delivering_Time,Note_Client")] Tbl_Orders tbl_Orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Ingredients = new SelectList(db.Tbl_Ingredients, "Id_Ingredients", "Name_Ingredients", tbl_Orders.Id_Ingredients);
            ViewBag.Id_Order = new SelectList(db.Tbl_Orders, "Id_Order", "Adress_Client", tbl_Orders.Id_Order);
            ViewBag.Id_Product = new SelectList(db.Tbl_Products, "Id_Product", "Nome_Product", tbl_Orders.Id_Product);
            ViewBag.Id_User = new SelectList(db.Tbl_Users, "Id_User", "Name_User", tbl_Orders.Id_User);
            return View(tbl_Orders);
        }

        // GET: Tbl_Orders/Data ----------------------------------------------------------------------------
        public JsonResult GetOrders(DateTime? Data)
        {
            List<Tbl_Orders> Orders = new List<Tbl_Orders>();

            //FILTRO SU USER ADMIN 
            //ritornerà la data se essa è disponibile

            if (User.IsInRole("Admin"))
                if (Data.HasValue)
                    // return Json(Orders.Where(o => DateTime.Parse(o.Delivering_Time).Date == Data.Value.Date).ToList(), JsonRequestBehavior.AllowGet);
                    //  else
                    return Json(Orders, JsonRequestBehavior.AllowGet);

            //FILTRO SU USER CLIENTE
            // Ritornerà la data all'utente Cliente che richiede la data se essa è disponibile

            int Id_Product = Int32.Parse(Session["Id_Product"].ToString());
            return Json(Orders.Where(o => o.Id_Product == Id_Product && o.IsFulfilled == false), JsonRequestBehavior.AllowGet);

        }

        // GET: Tbl_Orders/Cost -----------------------------------------------------------------------------
         /*
        public decimal GetCostOrder(int Id_Product)
         {
           return db.Tbl_Products.Find(Id_Product).Price_Product.Value;     
         }
         */

        // GET: Tbl_Orders/Confirmed ------------------------------------------------------------------------
        public ActionResult CofirmedOrder()
        {
            int id = Int32.Parse(Session["Id_User"].ToString());
            List<Tbl_Orders> Orders = db.Tbl_Orders.Where(o => o.Id_Product == id && o.IsFulfilled == false).ToList();

            foreach (Tbl_Orders Order in Orders)
            {
                Order.IsConfirmed = true;
                db.Entry(Order).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();

            return RedirectToAction("Index", "Home");

        }
        public ActionResult FulfilledOrder(int id)
        {
            Tbl_Orders order = db.Tbl_Orders.Find(id);
            order.IsFulfilled = true;
            db.Entry(order).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "Home");

        }
        // GET: Tbl_Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Orders tbl_Orders = db.Tbl_Orders.Find(id);
            if (tbl_Orders == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Orders);
        }

        // POST: Tbl_Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Orders tbl_Orders = db.Tbl_Orders.Find(id);
            db.Tbl_Orders.Remove(tbl_Orders);
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
