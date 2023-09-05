using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemoWeb2.Models;

namespace DemoWeb.Controllers
{
    public class CategoriesController : Controller
    {
        DBSportStore1Entities database = new DBSportStore1Entities();
        // GET: Categories
        public ActionResult Index()
        {
            var categories = database.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                database.Categories.Add(category);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("LỖI TẠO MỚI CATEGORY");
            }
        }
        //public ActionResult Details(int id)
        //{
        //    var category = database.Categories.Where(c => c.Id == id).FirstOrDefault();
        //    return View(category);
        //}

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = database.Categories.Where(c => c.Id == id).FirstOrDefault();
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(int id, Category category)
        {
            database.Entry(category).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new
               HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = database.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var category = database.Categories.Where(c => c.Id == id).FirstOrDefault();
                database.Categories.Remove(category);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Không xóa được do có liên quan đến bảng khác");
            }
        }
    }
}