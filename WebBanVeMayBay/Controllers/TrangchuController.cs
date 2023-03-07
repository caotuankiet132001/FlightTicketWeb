using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanVeMayBay.Models;
namespace WebBanVeMayBay.Controllers
{
    public class TrangchuController : Controller
    {
        private WebBanVeMayBayDBContext db = new WebBanVeMayBayDBContext();
        // GET: Trangchu
        public ActionResult Index()
        {
            var list = db.Products
                .Where(p => p.Status == 1)
                .Join(
                    db.Categories,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new ProductCategory
                    {
                        Id = p.Id,
                        Name = p.Name,
                        CatId = p.CatId,
                        //Slug = p.Slug,
                        Detail = p.Detail,
                        //MetaDes = p.MetaDes,
                        //MetaKey = p.MetaKey,    
                        //Number = p.Number,
                        Price = p.Price,
                        //Pricesale = p.Pricesale,
                        Created_At = p.Created_At,
                        //Created_By = p.Created_By,
                        Update_At = p.Update_At,
                        //Update_By = p.Update_By,
                        Status = p.Status,
                        MidAir = p.MidAir,
                        BreakTime = p.BreakTime,
                        Seat1 = p.Seat1,
                        Seat2 = p.Seat2,
                        OnAir = p.OnAir,
                        PlanTo = p.PlanTo,
                        PlanFrom = p.PlanFrom,
                        DateTime = p.DateTime,
                        CatName = c.Name
                    }
                )
                .Where(m => m.Status != 0).OrderByDescending(m => m.Created_At).ToList();
            var productForHomePageViewModel = new ProductForHomePageViewModel()
            {
                ListPlanFrom = list.Select(x => new SelectListItem
                {
                    Value = x.CatId.ToString(),
                    Text = x.CatName,
                }).ToArray(),
                ListProductCategory = list,
                SeletedPlanFromId = default,
            };
            return View(productForHomePageViewModel);
        }
        public ActionResult Search2(string SeletedPlanFromId, string SeletedPlanToId, DateTime date, DateTime date2)
        {

            var list = (db.Products
                       .Where(s => s.PlanFrom.Contains(SeletedPlanFromId) && s.PlanTo.Contains(SeletedPlanToId) ||
                               s.PlanFrom.Contains(SeletedPlanToId) && s.PlanTo.Contains(SeletedPlanFromId)
                               && DbFunctions.TruncateTime(s.DateTime) == date.Date
                               || DbFunctions.TruncateTime(s.DateTime) == date2.Date))
                       .ToList();
            @ViewBag.c = list;
            return View(list);
        }
        public ActionResult Search(string SeletedPlanFromId, string SeletedPlanToId, DateTime date)
        {
            var list = (db.Products
                       .Where(s => s.PlanFrom.Contains(SeletedPlanFromId)
                               && s.PlanTo.Contains(SeletedPlanToId)
                               && DbFunctions.TruncateTime(s.DateTime) == date.Date))
                       .ToList();
            @ViewBag.c = list;
            return View(list);
        }
        [HttpGet]
        public ActionResult DatVe(int id, string name, string date, string time, int Price)
        {
            var datve = new Datve()
            {
                Id = id,
                Name = name,
                Date = date,
                Time = time,
                Price = Price,
            };
            return View(datve);
        }
        public ActionResult Complain()
        {
            ViewBag.ListCat = new SelectList(db.Complain.ToList(), "Id", "Name");
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Complain(Complain complain)
        {
            if (ModelState.IsValid)
            {
                complain.CreateSend = DateTime.Now;
                complain.status = 0; //chưa giải quyết
                db.Complain.Add(complain);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(db.Complain.ToList(), "Id", "Name");
            return View(complain);
        }

        public ActionResult TicketSearch()
        {
            return View();
        }
        public ActionResult SearchTicketResult(string ClientId, string PhoneNumber)
        {
            var list = (db.Ticket.
                Where(s => s.ClientId == (ClientId) &&
                s.PhoneNumber == PhoneNumber));
            @ViewBag.c = list;
            return View(list);
        }
        public ActionResult Huyve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }
        [HttpPost, ActionName("Huyve")]
        [ValidateAntiForgeryToken]
        public ActionResult Huyve(int id)
        {
            Ticket ticket = db.Ticket.Find(id);
            db.Ticket.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}