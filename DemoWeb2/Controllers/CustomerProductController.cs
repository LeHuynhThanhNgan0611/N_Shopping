using DemoWeb2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using PagedList;
using Facebook;


namespace DemoWeb2.Controllers
{
    public class CustomerProductController : Controller
    {
        private DBSportStore1Entities db = new DBSportStore1Entities();
        // GET: Products
        public ActionResult Index(string searchString)
        {
            //var products = db.Products;
            //return View(products.ToList());
            var items = db.Products.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.NamePro.Contains(searchString)).ToList();
            }

            return View(items);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new
               HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult GetProductsByCategory()
        {
            var categories = db.Categories.ToList();
            return PartialView("CategoriesParitialView", categories);
        }
        public ActionResult GetProductsByCategory1()
        {
            var categories = db.Categories.ToList();
            return PartialView("_CateFooterView", categories);
        }
        public ActionResult GetProductsByCateId(int id)
        {
            var products = db.Products.Where(p => p.Category1.Id == id).ToList();
            return View("Index", products);
        }

        public ActionResult Contact()
        {
            return View();
        }
        //public ActionResult Shop()
        //{
        //    var products = db.Products;
        //    return View(products.ToList());
        //}
        public ActionResult Shop(string currentFilter, string SearchString, int? page)
        {
            //var products = db.products.where(n => n.namepro.contains(searchstring)).tolist();
            //return view(products);
            var lstProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstProduct = db.Products.Where(n => n.NamePro.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = db.Products.ToList();
            }
            ViewBag.CurrentProduct = SearchString;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            lstProduct = lstProduct.OrderByDescending(n => n.ProductID).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }

        //public ActionResult ShareOnFacebook()
        //{
        //    // Tạo đường dẫn của trang cần chia sẻ
        //    var url = "http://localhost:59750/CustomerProduct/Details/";

        //    // Khởi tạo đối tượng FacebookClient với Access Token của ứng dụng
        //    var fb = new FacebookClient("{access-token}");

        //    // Tạo đối tượng parameters để truyền vào phương thức FacebookClient.Post()
        //    var parameters = new Dictionary<string, object>();
        //    parameters["link"] = url;

        //    // Thực hiện chia sẻ nội dung lên Facebook
        //    dynamic result = fb.Post("me/feed", parameters);

        //    // Trả về view báo hiệu chia sẻ thành công
        //    return View();
        //}

    }
}