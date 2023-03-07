using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanVeMayBay.Models;

namespace WebBanVeMayBay.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        WebBanVeMayBayDBContext db = new WebBanVeMayBayDBContext();
        public ActionResult SearchResult(string key)
        {
            var listCB = db.Products.Where(n => n.PlanFrom.Contains(key));
            return View(listCB.OrderBy(n=>n.Date));
        }
    }
}