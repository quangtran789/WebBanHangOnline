using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;
using PagedList;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Products
        public ActionResult Index(int? page)
        {
            IEnumerable<Product> items = db.Products.OrderByDescending(x => x.Id);
            var pageSize = 10;
            var pageIndex = page ?? 1;
            var pagedItems = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageIndex;
            return View(pagedItems);
        }

        public ActionResult Add()
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, List<string> Images, List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                // Tạo bản sao của sản phẩm
                Product newProduct = CloneProduct(model);

                // Thực hiện các thay đổi trên sản phẩm mới
                newProduct.ProductCategoryId = 3;
                newProduct.CreatedDate = DateTime.Now;
                newProduct.ModifierDate = DateTime.Now;
                newProduct.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);

                // Lưu sản phẩm mới vào cơ sở dữ liệu
                db.Products.Add(newProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            return View(model);
        }

        // Các phương thức khác (Edit, Delete, IsActive, IsHome, IsSale) tương tự như trước.

        // Phương thức tạo bản sao của sản phẩm
        private Product CloneProduct(Product original)
        {
            return new Product
            {
                Id = original.Id,
                Title = original.Title,
                ProductCode = original.ProductCode,
                Description = original.Description,
                Detail = original.Detail,
                Image = original.Image,
                Price = original.Price,
                OriginalPrice = original.OriginalPrice,
                PriceSale = original.PriceSale,
                Quantity = original.Quantity,
                IsHome = original.IsHome,
                IsSale = original.IsSale,
                IsFeature = original.IsFeature,
                IsHot = original.IsHot,
                IsActive = original.IsActive,
                ProductCategoryId = original.ProductCategoryId,
                SepTitle = original.SepTitle,
                SeoDescription = original.SeoDescription,
                SeoKeywords = original.SeoKeywords,
            };
        }
    }
}
