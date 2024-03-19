using System;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    // Interface cho Proxy và Real Subject
    public interface IProductCategoryController
    {
        ActionResult Index();
        ActionResult Add();
        ActionResult Add(ProductCategory model);
        ActionResult Edit(int id);
        ActionResult Edit(ProductCategory model);
    }

    // Real Subject
    public class RealProductCategoryController : Controller, IProductCategoryController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var items = db.ProductCategories;
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifierDate = DateTime.Now;
                model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                db.ProductCategories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var item = db.ProductCategories.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                model.ModifierDate = DateTime.Now;
                model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                db.ProductCategories.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }

    // Proxy
    public class ProductCategoryControllerProxy : IProductCategoryController
    {
        private readonly RealProductCategoryController _realProductCategoryController;

        public ProductCategoryControllerProxy()
        {
            _realProductCategoryController = new RealProductCategoryController();
        }

        // Một số logic kiểm tra quyền truy cập có thể được thêm vào các phương thức dưới đây

        public ActionResult Index()
        {
            // Thực hiện kiểm tra quyền truy cập trước khi gọi phương thức Index() của Real Subject
            return _realProductCategoryController.Index();
        }

        public ActionResult Add()
        {
            // Thực hiện kiểm tra quyền truy cập trước khi gọi phương thức Add() của Real Subject
            return _realProductCategoryController.Add();
        }

        public ActionResult Add(ProductCategory model)
        {
            // Thực hiện kiểm tra quyền truy cập trước khi gọi phương thức Add(model) của Real Subject
            return _realProductCategoryController.Add(model);
        }

        public ActionResult Edit(int id)
        {
            // Thực hiện kiểm tra quyền truy cập trước khi gọi phương thức Edit(id) của Real Subject
            return _realProductCategoryController.Edit(id);
        }

        public ActionResult Edit(ProductCategory model)
        {
            // Thực hiện kiểm tra quyền truy cập trước khi gọi phương thức Edit(model) của Real Subject
            return _realProductCategoryController.Edit(model);
        }
    }
}