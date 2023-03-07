using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanVeMayBay.Models;
namespace WebBanVeMayBay.Areas.Admin.Controllers
{

}


//{
//    public class AuthController : Controller
//{
//    // GET: Admin/Auth
//    WebBanVeMayBayDBContext db = new WebBanVeMayBayDBContext();
//    public ActionResult Login()
//    {
//        if (!Session["UserAdmin"].Equals(""))
//        {
//            return RedirectToAction("Index", "Product");
//        }
//        ViewBag.StrError = "";
//        return View();
//    }
//    [HttpPost]
//    public ActionResult Login(FormCollection field)
//    {
//        string strError = "";
//        string username = field["username"];
//        string password = MyString.ToMD5(field["password"]);

//        User user = db.Users
//            .Where(m => m.Status == 1 && m.UserName == username)
//            .FirstOrDefault();
//        if (user == null)
//        {
//            strError = "Tên đăng nhập không tồn tại";
//        }
//        else
//        {
//            if (user.UserPassword.Equals(password))
//            {
//                Session["UserAdmin"] = user.UserName;
//                Session["UserId"] = user.UserId;
//                Session["FullName"] = user.UserFullName;
//                return RedirectToAction("Index", "Product");
//            }
//            else strError = "Mật khẩu không chính xác";
//        }

//        ViewBag.StrError = "<span class='text-danger'>" + strError + "</span>";
//        return View();
//    }
//    public ActionResult Logout()
//    {
//        Session["UserAdmin"] = "";
//        Session["UserId"] = "";
//        Session["FullName"] = "";
//        return RedirectToAction("Login");
//    }
//}
//}