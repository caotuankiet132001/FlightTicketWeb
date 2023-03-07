using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebBanVeMayBay.Models;

namespace WebBanVeMayBay.Areas.Admin.Helper
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var userId = Convert.ToString(httpContext.Session["UserId"]);
            if (!string.IsNullOrEmpty(userId))
                using (var context = new WebBanVeMayBayDBContext())
                {
                    var userRole = (from u in context.Users
                                    join r in context.Roles on u.RoleId equals r.RoleId
                                    where u.UserId == userId
                                    select new
                                    {
                                        r.RoleName
                                    }).FirstOrDefault();

                    //if (userRole != null)
                    //{
                    //    foreach (var role in allowedroles)
                    //    {
                    //        if (role == userRole.RoleName) return true;
                    //    }
                    //}

                    foreach (var role in allowedroles)
                    {
                        if (role == userRole?.RoleName) return true;
                    }
                }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Home" },
                    { "action", "UnAuthorized" }
               });
        }
    }
}