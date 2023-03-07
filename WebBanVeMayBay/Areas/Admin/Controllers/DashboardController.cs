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
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        private WebBanVeMayBayDBContext db = new WebBanVeMayBayDBContext();
        [CustomAuthorize("QLPH", "QLV", "QLCB", "QLLCB", "ADMIN")]
        public ActionResult Index()
        {
            var list = db.Products
                .Join(
                    db.Categories,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new ProductCategory
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
                .Where(m => m.Status != 0).OrderByDescending(m => m.Created_At).ToList();
            return View(list);

        }
    }
}