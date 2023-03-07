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
    public class TicketController : Controller
    {
        private WebBanVeMayBayDBContext db = new WebBanVeMayBayDBContext();
        // GET: Admin/Ticket
        [CustomAuthorize("QLV", "ADMIN")]
        public ActionResult Index(Ticket ticket)
        {
            var list = db.Ticket
               .OrderByDescending(c => c.CreateDate)
               .ToList();
            return View("Index", list);
        }
        [CustomAuthorize("QLV", "ADMIN")]
        public ActionResult Status(int id)
        {
            Ticket ticket = db.Ticket.Find(id);
            int status = (ticket.Status == 0) ? 1 : 0;
            ticket.Status = status;

            if (ticket.Status == 1)
            {
                Product product = db.Products.Find(ticket.PlanId);
                product.Seat1++;
                product.Seat2--;

                Client client = db.Clients.Where(x => x.ClientId == ticket.ClientId).FirstOrDefault();

                if (client == null)
                {
                    client = new Client();
                    client.ClientId = ticket.ClientId;
                    client.ClientName = ticket.ClientName;
                    client.PhoneNumber = ticket.PhoneNumber;
                    client.Point = 1;
                    db.Clients.Add(client); 
                    db.SaveChanges();
                }
                else
                {
                    if(client.Point >=10)
                    {
                        ticket.Price -= 100000;
                        client.Point -= 10;
                        ViewBag.c = "Đã áp dụng ưu đãi giảm 100.000đ";
                    }
                    client.Point++;
                }
            }
            db.Entry(ticket).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Ticket");
        }
        [CustomAuthorize("QLV", "ADMIN")]
        public ActionResult Delete(int? id)
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
        [CustomAuthorize("QLV", "ADMIN")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Ticket ticket = db.Ticket.Find(id);
            db.Ticket.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [CustomAuthorize("QLV", "ADMIN")]
        public ActionResult Filter(string Filter)
        {
            if (Filter == "Đã Thanh Toán")
            {
                var list = db.Ticket
               .Where(c => c.Status == 1)
               .OrderByDescending(c => c.CreateDate)
               .ToList();
                return View("Index", list);
            }
            else
            {
                var list = db.Ticket
               .Where(c => c.Status == 0)
               .OrderByDescending(c => c.CreateDate)
               .ToList();
                return View("Index", list);
            }

        }
        [CustomAuthorize("QLV", "ADMIN")]
        public ActionResult BillExport(int id)
        {
            Ticket ticket = db.Ticket.Find(id);
            Bill bill = new Bill();
            bill.TicketId = ticket.Id;
            bill.PlanId = ticket.PlanId;
            bill.PlanName = ticket.PlanName;
            bill.Time = ticket.Time;
            bill.Date = ticket.Date;
            bill.Price = ticket.Price;
            bill.PhoneNumber = ticket.PhoneNumber;
            bill.ClientId = ticket.ClientId;
            bill.ClientName = ticket.ClientName;
            return View(bill);
        }
        [CustomAuthorize("QLV", "ADMIN")]
        public ActionResult TicketSearch()
        {
            return View();
        }
        [CustomAuthorize("QLV", "ADMIN")]
        public ActionResult TicketSearchMCB()
        {
            return View();
        }
        [CustomAuthorize("QLV", "ADMIN")]
        public ActionResult SearchTicketResult(string ClientId, string PhoneNumber)
        {
            var list = (db.Ticket.
                Where(s => s.ClientId == (ClientId) &&
                s.PhoneNumber == PhoneNumber));
            @ViewBag.c = list;
            return View(list);
        }
        [CustomAuthorize("QLV", "ADMIN")]
        public ActionResult MCBSearchTicketResult(int PlanId)
        {
            var list = (db.Ticket.
                Where(s => s.PlanId.Equals(PlanId)));
            @ViewBag.c = list;
            return View(list);
        }







    }
}