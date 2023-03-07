using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanVeMayBay.Models;

namespace WebBanVeMayBay.Controllers
{
    public class DatVeController : Controller
    {
        private WebBanVeMayBayDBContext db = new WebBanVeMayBayDBContext();
        // GET: DatVe
        [HttpGet]
        public ActionResult Index(int Id, string Name, string Date, string Time, int Price, string ClientName, string ClientId, string PhoneNumber)
        {
            var ticket = new Ticket()
            {
                PlanId = Id,
                ClientId = ClientId,
                PhoneNumber = PhoneNumber,
                ClientName = ClientName,
                PlanName = Name,
                Date = Date,
                Time = Time,
                Price = Price,
            };
            return View(ticket);
        }
        //public ActionResult CreateTicket()
        //{
        //    ViewBag.ListCat = new SelectList(db.Categories.ToList(), "Id", "Name");
        //    return View();
        //}

        public ActionResult CreateTicket(int PlanId, string PlanName, string Date, string Time, int Price, string ClientName, string ClientId, string PhoneNumber)
        {
            var ticket = new Ticket()
            {
                PlanId = PlanId,
                ClientId = ClientId,
                PhoneNumber = PhoneNumber,
                ClientName = ClientName,
                PlanName = PlanName,
                Date = Date,
                Time = Time,
                Price = Price,
            };
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTicket(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.Status = 0;
                ticket.CreateDate = DateTime.Now;
                db.Ticket.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index", "Trangchu");
            }
            ViewBag.ListCat = new SelectList(db.Ticket.ToList(), "Id", "Name");
            return View(ticket);
        }

    }
}