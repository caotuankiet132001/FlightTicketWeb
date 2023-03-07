using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanVeMayBay.Models;

namespace WebBanVeMayBay.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            if (ModelState.IsValid)
            {
                using (var context = new WebBanVeMayBayDBContext())
                {
                    User users = context.Users
                                       .Where(u => u.UserName == UserName && u.Password == Password)
                                       .FirstOrDefault();
                    if (users != null)
                    {
                        Session["UserName"] = users.UserName;
                        Session["UserId"] = users.UserId;
                        if(users.UserName == "QLCB")
                        {
                            return RedirectToAction("Index", "Product");
                        }
                        else if(users.UserName == "QLLCB")
                        {
                            return RedirectToAction("Index", "Category");
                        }
                        else if (users.UserName == "QLV")
                        {
                            return RedirectToAction("Index", "Ticket");
                        }
                        else if (users.UserName == "QLPH")
                        {
                            return RedirectToAction("Index", "Complain");
                        }
                        else
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid User Name or Password");
                        return View();
                    }
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult LogOff(Client client)
        {
            Session["UserName"] = string.Empty;
            Session["UserId"] = string.Empty;
            return RedirectToAction("Login", "Account");
        }
    }
}
