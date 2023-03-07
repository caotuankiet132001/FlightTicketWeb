using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanVeMayBay.Areas.Admin.Helper;
using WebBanVeMayBay.Models;

namespace WebBanVeMayBay.Areas.Admin.Controllers
{
    [CustomAuthenticationFilter]
    public class ProductController : Controller
    {
        private WebBanVeMayBayDBContext db = new WebBanVeMayBayDBContext();

        // GET: Admin/Product
        [CustomAuthorize("QLCB", "ADMIN")]
        public ActionResult Index(Product product)
        {
            var list = db.Products
                .Join(
                    db.Categories,
                    p=>p.CatId,
                    c=>c.Id,
                    (p,c) => new ProductCategory
                    {
                        Id = p.Id,
                        Name = p.Name,
                        CatId = p.CatId,
                        Detail = p.Detail,
                        Number = p.Number,
                        Price = p.Price,
                        Pricesale = p.Pricesale,
                        Created_At = p.Created_At,
                        Created_By = p.Created_By,
                        Update_At = p.Update_At,
                        Update_By = p.Update_By,
                        Status = p.Status,
                        DateTime = p.DateTime,
                        MidAir = p.MidAir,
                        BreakTime = p.BreakTime,
                        Seat1 = p.Seat1,
                        Seat2 = p.Seat2,
                        OnAir = p.OnAir,
                        PlanTo = p.PlanTo,
                        PlanFrom = p.PlanFrom,
                        CatName = c.Name,
                    }
                )
                .Where(m => m.Status != 0).OrderByDescending(m=>m.Created_At).ToList();
            return View(list);
        }
        [CustomAuthorize("QLCB", "ADMIN")]
        // GET: Admin/Product/Details/5
        public List<Product> Search()
        {
            return db.Products.SqlQuery("Select * from Product").ToList();
        }
        [CustomAuthorize("QLCB", "ADMIN")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [CustomAuthorize("QLCB", "ADMIN")]
        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(db.Categories.ToList(), "Id", "Name");
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthorize("QLCB", "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Created_At = DateTime.Now;
                //product.Created_By = int.Parse(Session["UserId"].ToString());
                product.Update_At = DateTime.Now;
                //product.Update_By = int.Parse(Session["UserId"].ToString());
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(db.Categories.ToList(), "Id", "Name");
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        [CustomAuthorize("QLCB", "ADMIN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCat = new SelectList(db.Categories.ToList(), "Id", "Name");
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthorize("QLCB", "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Update_At = DateTime.Now;
                //product.Update_By = int.Parse((string)Session["UserId"]);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ListCat = new SelectList(db.Categories.ToList(), "Id", "Name");
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        [CustomAuthorize("QLCB", "ADMIN")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [CustomAuthorize("QLCB", "ADMIN")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            bool isHaveTicket = db.Ticket.Any(x => x.PlanId == id);
            if (!isHaveTicket)
            {
                ViewData["ErrorDeleteProduct"] = null;
                db.Products.Remove(product);
                db.SaveChanges();
            }
            else
            {
                TempData["ErrorDeleteProduct"] = "Không thể xóa chuyến bay này vì đã có vé đặt";
            }
            return RedirectToAction("Index");
        }
        [CustomAuthorize("QLCB", "ADMIN")]
        public ActionResult Status(int id)
        {
            Product product = db.Products.Find(id);
            int status = (product.Status == 1) ? 2 : 1;
            product.Status = status;
            //product.Update_By = int.Parse(Session["UserId"].ToString());
            product.Update_At = DateTime.Now;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Product");
        }
        //Xóa vào thùng rác, status - 0
        [CustomAuthorize("QLCB", "ADMIN")]
        public ActionResult DelTrash(int id)
        {
            Product product = db.Products.Find(id);
            product.Status = 0;
            //product.Update_By = 1;
            //product.Update_By = int.Parse(Session["UserId"].ToString());
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Product");
        }
        [CustomAuthorize("QLCB", "ADMIN")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [CustomAuthorize("QLCB", "ADMIN")]
        public ActionResult Trash()
        {
            var list = db.Products.Where(c => c.Status == 0)
                    .OrderByDescending(c => c.Created_At)
                    .ToList();
            return View("Trash", list);
        }
        [CustomAuthorize("QLCB", "ADMIN")]
        public ActionResult Restore(int id)
        {
            Product product = db.Products.Find(id);
            product.Status = 1;
            //category.Update_By = int.Parse(Session["UserAdmin"].ToString());
            product.Update_At = DateTime.Now;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
