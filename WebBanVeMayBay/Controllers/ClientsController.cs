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
    public class ClientsController : Controller
    {
        private WebBanVeMayBayDBContext db = new WebBanVeMayBayDBContext();

        // GET: Clients
        public ActionResult Filter(string Filter)
        {
            if (Filter == "Đã giải quyết")
            {
                var list = db.Complain
               .Where(c => c.status == 1)
               .OrderByDescending(c => c.CreateSend)
               .ToList();
                return View("Complain", list);
            }
            else
            {
                var list = db.Complain
               .Where(c => c.status == 0)
               .OrderByDescending(c => c.CreateSend)
               .ToList();
                return View("Complain", list);
            }

        }
        public ActionResult Complain(int id)
        {
            Client clients = db.Clients.Where(x => x.Id == id).FirstOrDefault();
            var complain = db.Complain.Where(x => x.ClientId == clients.ClientId).ToList();
            if (ModelState.IsValid)
            {
                @ViewBag.c = complain;
                return View(complain);
            }
            return View(complain);
        }
        public ActionResult Info(int id)
        {
            Client clients = db.Clients.Where(x => x.Id == id).FirstOrDefault();
            return View(clients);
        }
        public ActionResult ChangePassword(int id)
        {
            Client clients = db.Clients.Where(x => x.Id == id).FirstOrDefault();
            return View(clients);
        }
        [HttpPost]
        public ActionResult ChangePassword(Client clients)
        {
            if (ModelState.IsValid)
            {
                clients.ClientPassword = clients.NewPassword;
                db.Entry(clients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DangXuat");
            }
            return View(clients);
        }
        [HttpPost]
        public ActionResult Update(Client clients)
        {
            if (ModelState.IsValid) 
            {
                db.Entry(clients).State = EntityState.Modified;
                db.SaveChanges();
                return View(clients);
            }
            return View(clients);
        }
        public ActionResult Ticket(int id)
        {
            Client clients = db.Clients.Where(x => x.Id == id).FirstOrDefault();
            var tickets = db.Ticket.Where(x => x.ClientId == clients.ClientId).ToList();
            if (ModelState.IsValid)
            {
                @ViewBag.c = tickets;
                return View(tickets);
            }
            return View(tickets);
        }

        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(string ClientId, string PhoneNumber)
        {
            if (ClientId == null || PhoneNumber == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Where(x => x.ClientId == ClientId).FirstOrDefault();
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientId,ClientName,PhoneNumber,Point")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientId,ClientName,PhoneNumber,Point")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult ClientSearch(string ClientId, string PhoneNumber)
        {
            return View();
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(Client client)
        {
            if (ModelState.IsValid)
            {
                Client client2 = client as Client;

                if (string.IsNullOrEmpty(client.ClientName))
                    ModelState.AddModelError(string.Empty, "Họ tên không được để trống");
                if (string.IsNullOrEmpty(client.ClientAccount))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(client.ClientPassword))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (string.IsNullOrEmpty(client.ClientEmail))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(client.PhoneNumber))
                    ModelState.AddModelError(string.Empty, "Điện thoại không được đểtrống");
                if (string.IsNullOrEmpty(client.ClientAddress))
                    ModelState.AddModelError(string.Empty, "Địa chỉ không được để trống");
                //Kiểm tra xem có người nào đã đăng kí với tên đăng nhập này hay chưa

                var client1 = db.Clients.FirstOrDefault(k => k.ClientAccount == client.ClientAccount);
                if (client1 != null)
                {
                    ModelState.AddModelError(string.Empty, "Tài khoản đã tồn tại");
                    return RedirectToAction("DangKy", "Clients");
                }
                if (ModelState.IsValid)
                {
                    db.Clients.Add(client);
                    db.SaveChanges();
                    return RedirectToAction("DangNhap", "Clients");
                }
            }
            return View(client);

        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        //Phần đăng nhập//
        [HttpPost]
        public ActionResult DangNhap(string ClientAccount, string ClientPassword)
        {
            if (ModelState.IsValid)
            {
                using (var context = new WebBanVeMayBayDBContext())
                {
                    Client clients = context.Clients
                                       .Where(u => u.ClientAccount == ClientAccount && u.ClientPassword == ClientPassword)
                                       .FirstOrDefault();
                    if (clients != null)
                    {
                        Session["ClientName"] = clients.ClientName;
                        Session["UserName"] = clients.ClientAccount;
                        Session["UserId"] = clients.ClientId;
                        Session["Id"] = clients.Id;
                        if (string.IsNullOrEmpty(clients.ClientAccount))
                            ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                        if (string.IsNullOrEmpty(clients.ClientPassword))
                            ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                        if (ModelState.IsValid)
                        {
                            return RedirectToAction("Index", "TrangChu");
                        }
                    }
                    return View(clients);
                }
            }
            return View();
        }
        public ActionResult DangXuat(Client client)
        {
            Session["UserName"] = string.Empty;
            Session["UserId"] = string.Empty;
            Session["ClientName"] = "";
            Session["UserName"] = "";
            Session["UserId"] = "";
            Session["Id"] = client.Id;
            return RedirectToAction("DangNhap", "Clients");
        }

    }
}
