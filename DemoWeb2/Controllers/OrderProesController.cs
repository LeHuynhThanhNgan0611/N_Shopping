using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DemoWeb2.Models;
using PagedList;

namespace DemoWeb2.Controllers
{
    public class OrderProesController : Controller
    {
        private DBSportStore1Entities db = new DBSportStore1Entities();

        // GET: OrderProes
        public ActionResult Index()
        {
            var orderProes = db.OrderProes.Include(o => o.Customer);
            return View(orderProes.ToList());
        }

        // GET: OrderProes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderPro orderPro = db.OrderProes.Include(o => o.Customer).FirstOrDefault(o => o.ID == id);
            if (orderPro == null)
            {
                return HttpNotFound();
            }

            // Truy vấn danh sách sản phẩm trong đơn hàng
            orderPro.Products = db.OrderDetails
                .Where(o => o.IDOrder == orderPro.ID)
                .Select(o => o.Product)
                .ToList();

            return View(orderPro);
        }

        // GET: OrderProes/Create
        public ActionResult Create()
        {
            ViewBag.IDCus = new SelectList(db.Customers, "IDCus", "NameCus");
            return View();
        }

        // POST: OrderProes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DateOrder,IDCus,AddressDeliverry")] OrderPro orderPro)
        {
            if (ModelState.IsValid)
            {
                db.OrderProes.Add(orderPro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCus = new SelectList(db.Customers, "IDCus", "NameCus", orderPro.IDCus);
            return View(orderPro);
        }

        // GET: OrderProes/Edit/5
        // GET: OrderProes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderPro orderPro = db.OrderProes.Include(o => o.Customer).FirstOrDefault(o => o.ID == id);
            if (orderPro == null)
            {
                return HttpNotFound();
            }

            // Truy vấn danh sách sản phẩm trong đơn hàng
            orderPro.Products = db.OrderDetails
                .Where(od => od.ID == orderPro.ID)
                .Select(od => od.Product)
                .ToList();

            // Truy vấn tất cả sản phẩm để sử dụng trong SelectList
            ViewBag.Products = new SelectList(db.Products, "ID", "NamePro");

            return View(orderPro);
        }


        // POST: OrderProes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateOrder,IDCus,AddressDeliverry")] OrderPro orderPro)
        {
            if (ModelState.IsValid)
            {
                // Xóa toàn bộ các sản phẩm trong đơn hàng
                db.OrderDetails.RemoveRange(db.OrderDetails.Where(od => od.ID == orderPro.ID));

                // Thêm lại danh sách sản phẩm mới
                foreach (var product in orderPro.Products)
                {
                    db.OrderDetails.Add(new OrderDetail { ID = orderPro.ID, IDProduct = product.ProductID});
                }

                db.Entry(orderPro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCus = new SelectList(db.Customers, "IDCus", "NameCus", orderPro.IDCus);
            return View(orderPro);
        }

        // GET: OrderProes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPro orderPro = db.OrderProes.Find(id);
            if (orderPro == null)
            {
                return HttpNotFound();
            }
            return View(orderPro);
        }

        // POST: OrderProes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderPro orderPro = db.OrderProes.Find(id);
            db.OrderProes.Remove(orderPro);
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
