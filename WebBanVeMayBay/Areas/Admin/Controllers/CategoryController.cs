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
    public class CategoryController : Controller
    {
        private WebBanVeMayBayDBContext db = new WebBanVeMayBayDBContext();

        // GET: Admin/Category
        [CustomAuthorize("QLLCB", "ADMIN")]
        public ActionResult Index()
        {
            var list = db.Categories.Where(c => c.Status != 0)
                .OrderByDescending(c => c.Created_At)
                .ToList();
            return View("Index", list);
        }
        [CustomAuthorize("QLLCB", "ADMIN")]
        public ActionResult Trash()
        {
            var list = db.Categories.Where(c => c.Status == 0)
                    .OrderByDescending(c => c.Created_At)
                    .ToList();
            return View("Trash", list);
        }

        // GET: Admin/Category/Details/5
        [CustomAuthorize("QLLCB", "ADMIN")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Category/Create
        [CustomAuthorize("QLLCB", "ADMIN")]
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(db.Categories.ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Categories.ToList(), "Orders", "Name");
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthorize("QLLCB", "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.ParentID == null)
                    category.ParentID = 0;
                category.Created_At = DateTime.Now;
                //category.Created_By = int.Parse(Session["UserAdmin"].ToString());
                category.Update_At = DateTime.Now;
                //category.Update_By = int.Parse(Session["UserAdmin"].ToString());
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            ViewBag.ListCat = new SelectList(db.Categories.ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Categories.ToList(), "Orders", "Name");
            return View(category);
        }

        // GET: Admin/Category/Edit/5
        [CustomAuthorize("QLLCB", "ADMIN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCat = new SelectList(db.Categories.ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Categories.ToList(), "Orders", "Name");
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthorize("QLLCB", "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                if(category.ParentID == null)
                    category.ParentID = 0;
                category.Update_At = DateTime.Now;
                //category.Update_By = int.Parse(Session["UserAdmin"].ToString());

                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(db.Categories.ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Categories.ToList(), "Orders", "Name");
            return View(category);
        }

        // GET: Admin/Category/Delete/5
        [CustomAuthorize("QLLCB", "ADMIN")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Delete/5
        [CustomAuthorize("QLLCB", "ADMIN")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Trash","Category");
        }
        [CustomAuthorize("QLLCB", "ADMIN")]
        public ActionResult Status(int id)
        {
            Category category = db.Categories.Find(id);
            int status = (category.Status == 1) ? 2 : 1;
            category.Status = status;
            //category.Update_By = int.Parse(Session["UserAdmin"].ToString());
            category.Update_At = DateTime.Now;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Xóa vào thùng rác, status - 0
        [CustomAuthorize("QLLCB", "ADMIN")]
        public ActionResult DelTrash(int id)
        {
            Category category = db.Categories.Find(id);
            category.Status = 0;
            //category.Update_By = int.Parse(Session["UserAdmin"].ToString());
            category.Update_At = DateTime.Now;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [CustomAuthorize("QLLCB", "ADMIN")]
        public ActionResult Restore(int id)
        {
            Category category = db.Categories.Find(id);
            category.Status = 1;
            //category.Update_By = int.Parse(Session["UserAdmin"].ToString());
            category.Update_At = DateTime.Now;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }

}
